using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public partial class Player
{
    [SerializeField] bool log;

    [SerializeField] Health health = new(100, 100, 100);

    [FoldoutGroup("Events")] public readonly UnityEvent onHealthChange = new();

    public Health Health => health;

    public void Damage(Damage damage)
    {
        if (log){
            Debug.Log(damage, this);
        }
        health.Damage(damage);
        UpdateHealth();
        if (health.currentHealth == 0){
            Die();
        }
    }

    public void Heal(Damage damage)
    {
        health.Heal(damage);
        UpdateHealth();
    }

    public void Die()
    {
        BroadcastMessage("PlayerDead", SendMessageOptions.DontRequireReceiver);
        gameObject.SetActive(false);
    }

    void UpdateHealth()
    {
        onHealthChange.Invoke();
    }

    public void AddProtection(DamageType protectionType)
    {
        health.damagedBy &= ~protectionType;
    }
}