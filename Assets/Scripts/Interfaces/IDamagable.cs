public interface IDamagable
{
    void Damage(Damage damage);

    void Heal(Damage damage)
    {
        // Can't be healed by default
    }
    Health Health { get; }
}