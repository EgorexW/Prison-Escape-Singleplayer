public interface IDamagable
{
    void Damage(Damage damage)
    {
        Health.Damage(damage);
        if (!Health.Alive){
            Die();
        }
    }

    void Die();

    void Heal(Damage damage)
    {
        // Can't be healed by default
    }
    Health Health { get; }
}