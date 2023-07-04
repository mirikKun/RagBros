using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 4.7f;
    [SerializeField] private float wallSlidingSpeed = 1.3f;

    [Header("Jump")] [SerializeField] private float jumpHeight = 7;
    [SerializeField] private float gravityScale = 3;
    [SerializeField] private float fallGravityScale = 5;
    [SerializeField] private Transform groundedChecker;
    [SerializeField] private Transform playerModel;

    [SerializeField] private Transform nearWallChecker;
    [SerializeField] private Transform nearHighWallChecker;
    [SerializeField] private float checkGroundRadius = 0.06f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D _rb;
    private PlayerAnimationController _playerAnimationController;
    private Transform _transform;


    private bool _isFlipped;

    private Vector3 _localScale;

    /// <summary>
    /// Time you will have grounded status after jump
    /// </summary>
    [SerializeField] private float rememberGroundedFor = 0.1f;

    [SerializeField] private float timeOfClimbing = 0.3f;
    private float _curTimeOfClimbing;


    private float _lastTimeGrounded;
    private int _xDirection = 1;
    private bool _facingRight;

    /// <summary>
    /// Time you will have grounded status after jump
    /// </summary>
    [SerializeField] private float jumpPressMaxTime = 0.8f;

    private float _jumpPressTimeCounter;
    private bool _jumpBtnPressed;
    private bool _jumpBtnDown;

    private bool _isGrounded;
    private bool _isNearWall;
    
    private CharacterState _curCharacterState;

    private enum AnimationName
    {
        Idle,
        Landing,
        Run,
        Walk
    }

    private enum CharacterState
    {
        Idle,
        Running,
        Jumping,
        Falling,
        WallSlicing,
        Climbing
    }


    private void Start()
    {
        _playerAnimationController = GetComponent<PlayerAnimationController>();
        _rb = GetComponentInParent<Rigidbody2D>();
        _localScale = playerModel.localScale;
    }

    private void Update()
    {
#if !UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            OnJumpButtonDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnJumpButtonUp();
        }
#endif
        Move();
        Jump();
        WallSliding();
        CheckIfNearTheWall();
        CheckIfGrounded();
        Climbing();
        PcInput();
    }

    /// <summary>
    /// Moving character
    /// </summary>
    private void Move()
    {
        if (_playerAnimationController.attackingAnimation)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            return;
        }

        _playerAnimationController.SetDefaultAnimationSpeed();

        if (_curCharacterState != CharacterState.Running)
        {
            _playerAnimationController.FreezeAnimation();
        }
        else
        {
            _playerAnimationController.SetAnimation(AnimationName.Run.ToString(), true);
        }

        _rb.velocity = new Vector2(_xDirection * runSpeed, _rb.velocity.y);
    }


    /// <summary>
    /// Checking right rotation of character
    /// </summary>
    private void Rotate()
    {
        _facingRight = _xDirection > 0;
        if (((_facingRight && _localScale.x < 0) || (!_facingRight && _localScale.x > 0)))
        {
            Flip();
        }
    }

    /// <summary>
    /// Turning character around
    /// </summary>
    private void Flip()
    {
        _localScale.x *= -1;
        playerModel.Rotate(0, 180, 0);
        _isFlipped = !_isFlipped;
        _xDirection = -_xDirection;
    }


    private void Climbing()
    {
        if (_curCharacterState == CharacterState.Climbing)
        {
            if (Time.time - _curTimeOfClimbing <= timeOfClimbing)
            {
                _rb.velocity = Vector2.zero;
            }
            else
            {
                JumpApplying();
            }
        }
    }


    private void StartClimbing()
    {
        _rb.gravityScale = 0;
        _curCharacterState = CharacterState.Climbing;
        _curTimeOfClimbing = Time.time;
    }

    private void WallSliding()
    {
        if (_curCharacterState == CharacterState.WallSlicing)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -wallSlidingSpeed);
        }
    }

    private void Jump()
    {
        if (_jumpBtnPressed)
        {
            if ((_isGrounded || Time.time - _lastTimeGrounded <= rememberGroundedFor))
            {
                JumpApplying();
            }
            else if (_isNearWall)
            {
                Flip();
                _rb.velocity = Vector2.zero;
                JumpApplying();
            }
        }

//fall acceleration
        if (_curCharacterState == CharacterState.Jumping)
        {
            _jumpPressTimeCounter -= Time.deltaTime;
            if (!_jumpBtnDown && _jumpPressTimeCounter > 0)
            {
                _rb.gravityScale = fallGravityScale;
            }

            if (_rb.velocity.y < 0)
            {
                _rb.gravityScale = fallGravityScale;
                _curCharacterState = CharacterState.Falling;
            }
        }
    }

    private void JumpApplying()
    {
        _jumpPressTimeCounter = jumpPressMaxTime;
        _curCharacterState = CharacterState.Jumping;
        _jumpBtnPressed = false;
        _rb.gravityScale = gravityScale;
        float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * _rb.gravityScale) * -2) * _rb.mass;
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    /// <summary>
    /// Apply jump after head jump
    /// </summary>
    public void HeadJump()
    {
        JumpApplying();
    }

    /// <summary>
    /// Checks if the character is touching the ground to see if he can jump
    /// </summary>
    private void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(groundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders)
        {
            _jumpPressTimeCounter = jumpPressMaxTime;
            _isGrounded = true;
            _rb.gravityScale = gravityScale;

            if (_curCharacterState != CharacterState.Jumping)
            {
                _curCharacterState = CharacterState.Running;
            }

            if (_isFlipped)
            {
                Flip();
            }
        }
        else
        {
            if (_isGrounded)
            {
                _lastTimeGrounded = Time.time;
            }

            if (_curCharacterState == CharacterState.Falling)
            {
                _rb.gravityScale = fallGravityScale;
            }

            _isGrounded = false;
        }
    }

    private void CheckIfNearTheWall()
    {
        Collider2D colliders = Physics2D.OverlapCircle(nearWallChecker.position, checkGroundRadius, wallLayer);
        _isNearWall = colliders;
        //auto jump on small obstacles or climp if that there is little left to get to the top
        if (_isNearWall && _curCharacterState != CharacterState.Jumping)
        {
            colliders = Physics2D.OverlapCircle(nearHighWallChecker.position, checkGroundRadius, wallLayer);
            if (colliders)
            {
                _curCharacterState = CharacterState.WallSlicing;
            }
            else if (_isGrounded)
            {
                JumpApplying();
            }
            else if (_curCharacterState == CharacterState.Falling)
            {
                StartClimbing();
            }
        }
    }

    /// <summary>
    /// Test inputs for pressing Jump down
    /// </summary>
    public void OnJumpButtonDown()
    {
        if (_playerAnimationController.attackingAnimation)
            return;
        _jumpBtnPressed = true;
        _jumpBtnDown = true;
    }

    /// <summary>
    /// Test inputs for pressing Jump up
    /// </summary>
    public void OnJumpButtonUp()
    {
        _jumpBtnPressed = false;
        _jumpBtnDown = false;
    }


    /// <summary>
    /// Test inputs for PC
    /// </summary>
    private void PcInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            OnJumpButtonDown();
        }

        if (Input.GetButtonUp("Jump"))
        {
            OnJumpButtonUp();
        }
    }
}