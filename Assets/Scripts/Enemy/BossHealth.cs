using UnityEngine;
using UnityEngine.UI;

public class BossHealth : EnemyHealth
{
    [SerializeField] private Image healthBar;
    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
        healthBar.fillAmount = _currentHealth / maxHealth;
    }
}
