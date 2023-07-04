
using Spine.Unity;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private string _currentAnimationName;
    public bool attackingAnimation;
    private SkeletonAnimation _skeletonAnimation;


    private void Start()
    {
        _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();

    }

    public void FreezeAnimation()
    {
        _skeletonAnimation.timeScale = 0;

    }
    public void SetDefaultAnimationSpeed()
    {
        _skeletonAnimation.timeScale = 1;
    }

    public void SetAnimationSpeed(float multiplayer)
    {
        _skeletonAnimation.timeScale = multiplayer;

    }
    
    public void SetAnimation(string animationName, bool loop)
    {
        if (_currentAnimationName == animationName)
            return;
        _currentAnimationName = animationName;

        _skeletonAnimation.state.SetAnimation(0, animationName, loop);
    }
}
