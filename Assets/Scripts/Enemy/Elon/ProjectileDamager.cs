using UnityEngine;

public class ProjectileDamager : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private ParticleSystem hitExplosionParticle;
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Projectile deal damage to character and destroying 
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        var healthController = other.gameObject.GetComponent<HealthController>();
        if (healthController)
        {
            healthController.GetDamage(damage);
        }

        collider2d.enabled = false;
        spriteRenderer.enabled = false;
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, 0.8f);
    }
}