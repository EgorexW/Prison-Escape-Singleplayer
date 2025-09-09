public interface IDamagable
{
    Health Health{ get; }
    void Damage(Damage damage);

    void Heal(Damage damage)
    {
        // Can't be healed by default
    }
}