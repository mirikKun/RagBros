using UnityEngine;

public class CekAI : EnemyAI
{
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private Rigidbody2D projectile;
    [SerializeField] private ParticleSystem shootBlowParticles;
    [SerializeField] private float projectileSpeed = 8;
    [SerializeField] private float distanceToShoot = 14.5f;
    [SerializeField] private float highToShoot=3;
    [SerializeField] private string shootAnimationName;

    [SerializeField] private float swingTime = 0.62f;
    private float _startOfSwingTime;

    [SerializeField] private float afterSwingAnimationTime = 0.3f;
    private float _startOfAfterSwingAnimationTime;

    [SerializeField] private float reloadTime = 1f;
    private float _startOfReloadTime;
    private bool _reloading;


    protected override void StateChoosing()
    {
        base.StateChoosing();
        switch (CurrentState)
        {
            case State.PreShooting:
                WaitingForShoot();
                break;
            case State.AfterShooting:
                ShootEnding();
                break;
        }
    }


    protected override void Moving()
    {
        
        base.Moving();
        Vector3 diff = EnemyTransform.position - Player.transform.position;
        if (_reloading)
        {
            if (Time.time - _startOfReloadTime >= reloadTime)
            {
                _reloading = false;
            }
        }
        else if (diff.x < distanceToShoot&&Mathf.Abs(diff.y)<=highToShoot)
        {
            StartShooting();
        }
    }

    private void StartShooting()
    {
        CurrentState = State.PreShooting;
        SetAnimation(shootAnimationName, false);
        _startOfSwingTime = Time.time;
    }

    private void WaitingForShoot()
    {

        if (Time.time - _startOfSwingTime >= swingTime)
        {
            shootBlowParticles.Play();
            Rigidbody2D newLaser = Instantiate(projectile, projectileSpawnPosition.position, Quaternion.identity);
            newLaser.velocity = Vector2.right * (-1 * projectileSpeed);
            EndShooting();
        }
    }

    private void EndShooting()
    {            

        CurrentState = State.AfterShooting;
        _startOfAfterSwingAnimationTime = Time.time;
    }

    private void ShootEnding()
    {
        if (Time.time - _startOfAfterSwingAnimationTime >= afterSwingAnimationTime)
        {
            StartMoving();
            _startOfReloadTime = Time.time;
            _reloading = true;
        }
    }
}