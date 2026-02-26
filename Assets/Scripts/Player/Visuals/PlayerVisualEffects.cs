using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class PlayerVisualEffects : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    
    [SerializeField] Volume staminaVolume;
    
    [SerializeField] float changeSpeed = 0.5f;
    
    bool staminaEffectsActive;

    void Awake()
    {
        player.playerEffects.onEffectsChange.AddListener(OnEffectsChange);
    }

    void Update()
    {
        if (staminaVolume != null){
            var newWeight = changeSpeed * (staminaEffectsActive ? 1 : -1) * Time.deltaTime + staminaVolume.weight;
            staminaVolume.weight = Mathf.Clamp01(newWeight);
        }
    }

    void OnEffectsChange()
    {
        staminaEffectsActive = false;
        foreach (var effect in player.playerEffects.ActiveEffects){
            if (effect.staminaPerSecond > 0){
                staminaEffectsActive = true;
            }
        }
    }
}
