using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerEffects : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Player player;

    [FoldoutGroup("Debug")] [ShowInInspector] List<PlayerEffect> activeEffects = new();
    
    public List<PlayerEffect> ActiveEffects => activeEffects.Copy();

    [FoldoutGroup("Events")] public UnityEvent onEffectsChange;

    void Update()
    {
        for (var i = activeEffects.Count - 1; i >= 0; i--){
            var effect = activeEffects[i];
            effect.duration -= Time.deltaTime;
            if (effect.endTime <= Time.time){
                RemoveEffect(i);
                continue;
            }
            player.playerHealth.Heal(effect.healPerSecond * Time.deltaTime);
            player.AddStamina(effect.staminaPerSecond * Time.deltaTime);
        }
    }

    void RemoveEffect(int i)
    {
        activeEffects.RemoveAt(i);
        onEffectsChange.Invoke();
    }

    public void ApplyEffect(PlayerEffect effect)
    {
        effect.endTime = Time.time + effect.duration;
        activeEffects.Add(effect);
        onEffectsChange.Invoke();
    }
}