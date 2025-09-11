using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Player player;
    
    [FoldoutGroup("Debug")][ShowInInspector] List<PlayerEffect> activeEffects = new();
    
    public void ApplyEffect(PlayerEffect effect)
    {
        effect.endTime = Time.time + effect.duration;
        activeEffects.Add(effect);
    }
    
    void Update()
    {
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            var effect = activeEffects[i];
            effect.duration -= Time.deltaTime;
            if (effect.endTime <= Time.time)
            {
                activeEffects.RemoveAt(i);
                continue;
            }
            player.Heal(effect.healPerSecond * Time.deltaTime);
            player.AddStamina(effect.staminaPerSecond * Time.deltaTime);
        }
    }
}