using System;
using UnityEngine;

[Serializable]
public struct Health
{
    public float health;
    public float maxHealth;
    public float absoluteMaxHealth;
    
    public bool Alive => health > 0;
    public Health(float health) : this(health, health, health) {}
    public Health(float health, float maxHealth, float absoluteMaxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        this.absoluteMaxHealth = absoluteMaxHealth;
    }
    public void Heal(Damage damage){
        Damage(-damage);
    }
    public void Damage(Damage damage){
        health -= damage.damage;
        maxHealth -= damage.permanentDamage;
        maxHealth = Mathf.Clamp(maxHealth, 0, absoluteMaxHealth);
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}