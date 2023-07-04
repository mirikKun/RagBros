using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetDamage(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GetDamage(other.gameObject);
    }

    private void GetDamage(GameObject other)
    {
        var healthController = other.GetComponent<HealthController>();
        if (healthController)
        {
            healthController.GetDamage(damage);
        } 
    }
}
