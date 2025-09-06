using System;
using UnityEngine;

[Serializable]
public struct Health
{
    public float health;
    public float maxHealth;
    public float absoluteMaxHealth;
    public DamageType damagedBy;
    
    public bool Alive => health > 0;
    public Health(float health) : this(health, health, health) {}
    public Health(float health, float maxHealth, float absoluteMaxHealth, DamageType damagedBy = DamageType.Physical)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        this.absoluteMaxHealth = absoluteMaxHealth;
        this.damagedBy = damagedBy;
    }
    public void Heal(Damage damage)
    {
        damage.Invert();
        Damage(damage, true);
    }
    public void Damage(Damage damage, bool ignoreImmunities = false){
        if (!ignoreImmunities){
            if (damagedBy == 0){
                Debug.LogWarning("This entity is invulnerable");
                return;
            }
            // Debug.Log($"Damaged by {damage.damageType}, damageable by {damagedBy}");
            if ((damagedBy & damage.damageType) == 0) {
                return;
            }
        }
        health -= damage.damage;
        maxHealth -= damage.permanentDamage;
        maxHealth = Mathf.Clamp(maxHealth, 0, absoluteMaxHealth);
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}