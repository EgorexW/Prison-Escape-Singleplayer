using System;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[Serializable]
[InlineProperty]
public struct Damage
{
    public float damage;
    [FormerlySerializedAs("permanentDamage")] public float pernamentDamage;
    public DamageType damageType;

    public bool IsDamage => damage > 0 || pernamentDamage > 0;
    public bool IsHeal => damage < 0 || pernamentDamage < 0;
    public bool IsZero => damage == 0 && pernamentDamage == 0;

    public void Invert()
    {
        damage = -damage;
        pernamentDamage = -pernamentDamage;
    }

    public Damage(float damage, float pernamentDamage = 0, DamageType damageType = DamageType.Physical)
    {
        this.damage = damage;
        this.pernamentDamage = pernamentDamage;
        this.damageType = damageType;
    }

    public static implicit operator float(Damage damage)
    {
        return damage.damage;
    }

    public static implicit operator Damage(float damage)
    {
        return new Damage(damage);
    }

    public static Damage operator *(Damage initialDamage, float value)
    {
        return new Damage(initialDamage.damage * value, initialDamage.pernamentDamage * value,
            initialDamage.damageType);
        ;
    }

    public override string ToString()
    {
        return $"Damage: {damage} Permanent Damage: {pernamentDamage}";
    }
}

[Flags]
public enum DamageType
{
    Physical = 1 << 0,
    Emp = 1 << 1,
    Poison = 1 << 2
}