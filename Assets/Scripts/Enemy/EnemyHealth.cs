using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 10;
    
    [SerializeField] private GameObject rootObject;
    [SerializeField] private float damageOffTime = 0.2f;

    [SerializeField] private bool canBeDamagedByHead;
    [SerializeField] private bool canBeStunnedByHead;

    private EnemyAI _enemyAI;
    private bool _canTakeDamage = true;

    protected float _currentHealth;
    private int _headDamage = 20;


    private void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
        _currentHealth = maxHealth;
    }

    /// <summary>
    /// Damage applying 
    /// </summary>
    public virtual void GetDamage(int damage)
    {
        if (!_canTakeDamage)
            return;
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
        _canTakeDamage = false;
        StartCoroutine(WaitForReadyToTakeDamage());
    }

    /// <summary>
    /// Receiving damage from jumping a character on the head 
    /// </summary>
    public void GetHeadDamage()
    {
        if (canBeStunnedByHead)
            _enemyAI.GetStun();
        if (canBeDamagedByHead)
            GetDamage(_headDamage);
    }

    /// <summary>
    /// Enemy being invisible after taking damage
    /// </summary>
    private IEnumerator WaitForReadyToTakeDamage()
    {
        yield return new WaitForSeconds(damageOffTime);
        _canTakeDamage = true;
    }


    private void Die()
    {
        Destroy(rootObject);
    }
}