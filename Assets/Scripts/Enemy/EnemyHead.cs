using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
    /// <summary>
    /// Get damage when character jump on head
    /// </summary>
    private void OnCollisionEnter2D(Collision2D other)
    {
        GetDamage(other.gameObject);
    }

    private void GetDamage(GameObject other)
    {
        var playerMovement = other.GetComponent<PlayerMovement>();
        if (playerMovement)
        {
            playerMovement.HeadJump();
            if(enemyHealth)
                enemyHealth.GetHeadDamage();
        }
    }
}
