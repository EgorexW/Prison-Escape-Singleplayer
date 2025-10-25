using System;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[Serializable]
[InlineProperty]
public struct Damage
{
    [FormerlySerializedAs("damage")] public float lightDamage;
    [FormerlySerializedAs("pernamentDamage")] [FormerlySerializedAs("permanentDamage")] public float heavyDamage;
    public DamageType damageType;

    public bool IsDamage => lightDamage > 0 || heavyDamage > 0;
    public bool IsHeal => lightDamage < 0 || heavyDamage < 0;
    public bool IsZero => lightDamage == 0 && heavyDamage == 0;

    public void Invert()
    {
        lightDamage = -lightDamage;
        heavyDamage = -heavyDamage;
    }

    public Damage(float lightDamage, float heavyDamage = 0, DamageType damageType = DamageType.Physical)
    {
        this.lightDamage = lightDamage;
        this.heavyDamage = heavyDamage;
        this.damageType = damageType;
    }

    public static implicit operator float(Damage damage)
    {
        return damage.lightDamage;
    }

    public static implicit operator Damage(float damage)
    {
        return new Damage(damage);
    }

    public static Damage operator *(Damage initialDamage, float value)
    {
        return new Damage(initialDamage.lightDamage * value, initialDamage.heavyDamage * value,
            initialDamage.damageType);
        ;
    }

    public override string ToString()
    {
        return $"Light Damage: {lightDamage}, Heavy Damage: {heavyDamage}";
    }

    public void SetZero()
    {
        lightDamage = 0;
        heavyDamage = 0;
    }
}

[Flags]
public enum DamageType
{
    Physical = 1 << 0,
    Electric = 1 << 1,
    Poison = 1 << 2
}