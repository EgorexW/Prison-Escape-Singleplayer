using UnityEngine;

public partial class Character
{
    [SerializeField] Health health = new Health(100, 100, 100);

    public Health Health => health;

    public void Damage(Damage damage)
    {   
        health.Damage(damage);
        UpdateHealth();
        if (health.health == 0){
            Die();
        }
    }
    public void Heal(Damage damage)
    {
        health.Heal(damage);
        UpdateHealth();
    }
    private void Die()
    {
        Destroy(gameObject);
    }

    private void UpdateHealth()
    {
        characterEvents.onHealthChange.Invoke();
    }
}
