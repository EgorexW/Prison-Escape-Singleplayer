using System;
using UnityEngine;

public class EffectItem : UseableItem
{
    [SerializeField] Damage damage;
    [SerializeField] Damage heal;
    [SerializeField] PlayerEffect effect;

    protected override void Apply()
    {
        player.playerHealth.Damage(damage);
        player.playerHealth.Heal(heal);
        player.playerEffects.ApplyEffect(effect);
        player.RemoveItem(Item);
        Destroy(gameObject);
    }
}

[Serializable]
public struct PlayerEffect
{
    public float duration;
    public Damage healPerSecond;
    public float staminaPerSecond;
    [HideInInspector] public float endTime;
}