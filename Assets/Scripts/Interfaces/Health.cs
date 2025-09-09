using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct Health
{
    [FormerlySerializedAs("health")] public float currentHealth;
    public float maxHealth;
    public float absoluteMaxHealth;
    public DamageType damagedBy;
    
    public bool Alive => currentHealth > 0;
    public Health(float currentHealth) : this(currentHealth, currentHealth, currentHealth) {}
    public Health(float currentHealth, float maxHealth, float absoluteMaxHealth, DamageType damagedBy = DamageType.Physical)
    {
        this.currentHealth = currentHealth;
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
        currentHealth -= damage.damage;
        maxHealth -= damage.permanentDamage;
        maxHealth = Mathf.Clamp(maxHealth, 0, absoluteMaxHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}