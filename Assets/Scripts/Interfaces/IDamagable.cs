public interface IDamagable
{
    void Damage(Damage damage);
    void Heal(Damage damage);
    Health GetHealth();
}