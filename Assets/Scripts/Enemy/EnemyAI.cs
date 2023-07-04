using UnityEngine;
using Spine.Unity;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float stunTime;
    private float _StartOfStunTime;
    [SerializeField] private string walkAimName;
    [SerializeField] private string stunAnimName;
    [SerializeField] private bool canBeStunnedByHead;
    [SerializeField] private float distanceMovementStart;
    protected Transform Player;
    private bool _attacking;
    private string _currentAnimationName;
    private SkeletonAnimation _skeletonAnimation;
    protected Transform EnemyTransform;

    /// <summary>
    /// States of enemy logic
    /// </summary>
    public enum State
    {
        Waiting,
        PreShooting,
        AfterShooting,
        Dying,
        Moving,
        Stun
    };


    protected State CurrentState;
    private bool _movingRight;

    private void Start()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        Player = FindObjectOfType<PlayerMovement>().transform;
        CurrentState = State.Waiting;
        EnemyTransform = transform;
    }
    
    /// <summary>
    /// Applying animation to the enemy
    /// </summary>
    /// <param name="animationName"></param>
    /// <param name="loop"></param>
    protected  void SetAnimation(string animationName, bool loop)
    {
        if (_currentAnimationName == animationName)
            return;
        _currentAnimationName = animationName;
        _skeletonAnimation.state.SetAnimation(0, animationName, loop);
    }

    private void Update()
    {
        StateChoosing();
    }

    protected virtual  void StateChoosing()
    {
        switch (CurrentState)
        {
            case State.Moving:
                Moving();
                break;
            case State.Stun:
                BeingInStun();
                break;
            case State.Waiting:
                WaitingForPlayer();
                break;
        }  
    }

    public void GetStun()
    {
        if (!canBeStunnedByHead)
            return;
        CurrentState = State.Stun;
        SetAnimation(stunAnimName, false);
        _StartOfStunTime = Time.time;
    }

    private void BeingInStun()
    {
        if (Time.time - _StartOfStunTime >= stunTime)
        {
            StartMoving();
        }
    }

    /// <summary>
    /// Wait while character will be at certain distance to the enemy
    /// </summary>
    private void WaitingForPlayer()
    {
        Vector3 diff = EnemyTransform.position-Player.transform.position;
        if (diff.x < distanceMovementStart)
        {
           StartMoving();
        }
    }
    protected void StartMoving()
    {
        SetAnimation(walkAimName, true);
        CurrentState = State.Moving;
    }
    /// <summary>
    /// The enemy is patrolling the area and does not see the character
    /// </summary>
    protected virtual void Moving()
    {
        EnemyTransform.position -= speed*  Time.deltaTime * Vector3.right;
    }

}