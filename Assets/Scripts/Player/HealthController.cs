using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Image healthBar;
    [SerializeField] private float damageOffTime = 0.3f;

    [SerializeField] private SceneController sceneController;
    private bool _canTakeDamage = true;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    /// <summary>
    /// Character takes damage
    /// </summary>
    public virtual void GetDamage(float damage)
    {
        if (!_canTakeDamage)
            return;
        _currentHealth -= damage;
        _canTakeDamage = false;
        if (healthBar)
            healthBar.fillAmount = _currentHealth / maxHealth;
        if (_currentHealth <= 0)
        {
            Die();
        }

        StartCoroutine(WaitForReadyToTakeDamage());
    }

    /// <summary>
    /// Character being invisible after taking damage
    /// </summary>
    private IEnumerator WaitForReadyToTakeDamage()
    {
        yield return new WaitForSeconds(damageOffTime);
        _canTakeDamage = true;
    }


    public virtual void HealthRecovery(float health)
    {
        _currentHealth += health;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }

        if (healthBar)
            healthBar.fillAmount = _currentHealth / maxHealth;
    }

    private void Die()
    {
        transform.position = Vector3.zero;
        Respawn();
    }

    private void Respawn()
    {
        sceneController.ReloadLevel();
    }
}