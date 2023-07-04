
using UnityEngine;
using Spine.Unity;

using Random = UnityEngine.Random;

public class ElonAI : MonoBehaviour
{
    [SerializeField] private Transform patrolPoint;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float laserSpeed=13;

    [SerializeField] private Transform teleportEffect;
    [SerializeField] private Rigidbody2D laser;
    
    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float positionOfPatrol = 4;
    [SerializeField] private float chaseDistance = 3;
    private Transform _player;
    private Transform _transform;

    private string _currentAnimationName;
    private SkeletonAnimation _skeletonAnimation;

    private Vector3 _positionToTeleport;
    
    private float _teleportAnimationTime = 0.5f;
    private float _timeBetweenTeleports = 2.5f;
    private float _timeInTeleportation = 0.7f;


    private float _shootTime = 0.33f;
    private float _delayTimeAfterShoot = 0.7f;

    private float _chillTime = 1;


    private float _curTimer = 0;
    private float _scaleX;
    private int _direction=-1;
    [SerializeField]private bool _teleportDisabled = false;



    private enum State
    {
        ChillAfterTeleport,
        Appearance,
        AppearanceDelay,
        Disappearance,
        ChillAfterShoot,
        Jump,
        Moon_Walk,
        Run,
        Shoot,
        Walk
    };

    private State _state;

    private void Start()
    {
        _transform = transform;

        
        _player = FindObjectOfType<PlayerMovement>().transform;
        _scaleX = _transform.localScale.x;

        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        _positionToTeleport= patrolPoint.position + Random.Range(-positionOfPatrol, positionOfPatrol) * Vector3.right;
        Flip(transform.position.x>_positionToTeleport.x);
        _state = State.Disappearance;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.ChillAfterTeleport:
                ChillAfterTeleport();
                break;               
            case State.Appearance:
                Appearance();
                break;            
            case State.AppearanceDelay:
                AppearanceDelay();
                break;
            case State.Disappearance:
                Disappearance();
                break;
            case State.Shoot:
                Shoot();
                break;                  
            case State.ChillAfterShoot:
                ChillAfterShoot();
                break;      
            case State.Jump:
                Jump();
                break;
            case State.Moon_Walk:
                Moon_Walk();
                break;
            case State.Run:
                Run();
                break;
      
            case State.Walk:
                Walk();
                break;
        }
    }


    private void ChillAfterTeleport()
    {
        _curTimer += Time.deltaTime;
        if (_curTimer >= _timeBetweenTeleports)
        {
            _curTimer = 0;
            _state = State.Disappearance;
            _positionToTeleport= patrolPoint.position + Random.Range(-positionOfPatrol, positionOfPatrol) * Vector3.right;
            Flip(transform.position.x>_positionToTeleport.x);
            SetAnimation(State.Disappearance.ToString(),false);
        }

        if (Vector2.Distance(transform.position, _player.position) < chaseDistance)
        {
            _curTimer = 0;
            Flip(transform.position.x>_player.position.x);
            
            _state = State.Shoot;
            SetAnimation(State.Shoot.ToString(),false);
        }

        if (_teleportDisabled)
        {
            _state = State.Walk;
            SetAnimation(State.Walk.ToString(),true);

        }
    }
    private void Appearance()
    {
        _curTimer += Time.deltaTime;
        if (_curTimer >= _teleportAnimationTime)
        {
            
            _curTimer = 0;
            _state = State.ChillAfterTeleport;
        }
    }

    private void AppearanceDelay()
    {
        _curTimer += Time.deltaTime;
        if (_curTimer >= _timeInTeleportation)
        {
            _curTimer = 0;
            _state = State.Appearance;
            collider2D.enabled = true;
            _transform.position = _positionToTeleport;
            SetAnimation(State.Appearance.ToString(),false);
        }
    }

    private void Disappearance()
    {
        _curTimer += Time.deltaTime;
        if (_curTimer >= _teleportAnimationTime)
        {
            teleportEffect.position = _positionToTeleport;
            teleportEffect.gameObject.SetActive(true);
            _curTimer = 0;
            _state = State.AppearanceDelay;
            collider2D.enabled = false;
        }
    }
    private void Shoot()
    {
        _curTimer += Time.deltaTime;
        if (_curTimer >= _shootTime)
        {
            _curTimer = 0;
            _state = State.ChillAfterShoot;
            Rigidbody2D newLaser= Instantiate(laser, shootPoint.position, Quaternion.identity);
            newLaser.velocity = Vector2.right * (_direction * laserSpeed);
        } 
    }    
    private void ChillAfterShoot()
    {
        _curTimer += Time.deltaTime;
        if (_curTimer >= _delayTimeAfterShoot)
        {
            _curTimer = 0;
            _state = State.Disappearance;
            
            _positionToTeleport = patrolPoint.position + positionOfPatrol*_direction * Vector3.right;
            Flip(transform.position.x>_positionToTeleport.x);
            
            SetAnimation(State.Disappearance.ToString(),false);
        } 
    }
    private void Jump()
    {
        
    }

    private void Moon_Walk()
    {
    }
    private void Run()
    {
    }
   
    private void Walk()
    {
        if (_direction==1 && transform.position.x > patrolPoint.position.x + positionOfPatrol)
        {
            Flip(true);
        }
        else if (_direction==-1 && transform.position.x < patrolPoint.position.x - positionOfPatrol)
        {
            Flip(false);
        }

        _transform.position += _direction*walkSpeed * Time.deltaTime * Vector3.right;
        if (!_teleportDisabled)
        {
            _curTimer = 0;
            _state = State.Disappearance;
            SetAnimation(State.Disappearance.ToString(),false);
        }
    }

    private void Flip(bool moveLeft)
    {
        var localScale = _transform.localScale;
        if (moveLeft)
        {
            _direction = -1;

            localScale = new Vector3(_scaleX, localScale.y, localScale.z);
        }
        else
        {
            _direction = 1;

            localScale = new Vector3(-_scaleX, localScale.y, localScale.z);
        }

        _transform.localScale = localScale;
    }
    private void SetAnimation(string animationName, bool loop)
    {
        if (_currentAnimationName == animationName)
            return;
        _currentAnimationName = animationName;
        _skeletonAnimation.state.SetAnimation(0, animationName, loop);
    }
}