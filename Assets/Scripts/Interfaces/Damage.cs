using System;
using Sirenix.OdinInspector;

[Serializable]
[BoxGroup, InlineProperty]
public struct Damage{
    public float damage;
    public float permanentDamage;

    public Damage(float damage) : this(damage, 0) {

    }

    public void Invert()
    {
        damage = -damage;
        permanentDamage = -permanentDamage;
    }
    public Damage(float damage, float permanentDamage){
        this.damage = damage;
        this.permanentDamage = permanentDamage;
    }
    public static implicit operator float(Damage damage){
        return damage.damage;
    }
    public static implicit operator Damage(float damage){
        return new Damage(damage);
    }
    public static Damage operator *(Damage initialDamage, float value){
        return new Damage(initialDamage.damage * value, initialDamage.permanentDamage * value);;
    }
    public override string ToString() => $"Damage: {damage} Permanent Damage: {permanentDamage}";
}