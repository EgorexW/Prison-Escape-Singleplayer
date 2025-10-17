using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Ascensions : MonoBehaviour
{
    public List<AscensionEffect> effects;

    [BoxGroup("References")] [Required] [SerializeField] CorridorSpawner corridorTrapsSpawner;

    [SerializeField] float tmpAscensionsNr = 7;

    public void SetupAscensions()
    {
        var activeAscensions = tmpAscensionsNr;
        for (int i = 0; i < activeAscensions; i++){
            if (effects.Count <= i){
                Debug.LogWarning("Run out of ascensions", this);
                break;
            }
            ApplyEffect(effects[i]);
        }
    }

    void ApplyEffect(AscensionEffect effect)
    {
        GameManager.i.gameTimeManager.gameTimeMinutes -= effect.gameTimeMinutesReduction;
        GameManager.i.Player.GetInventory().SetSize(GameManager.i.Player.GetInventory().GetSize()-effect.inventorySizeReduction);
        GameManager.i.trapsManager.trapChance += effect.trapChanceIncrease;
        GameManager.i.Player.playerHealth.Damage(effect.startDamage);
        corridorTrapsSpawner.spawnCount.x += effect.corridorTrapsIncrease;
        corridorTrapsSpawner.spawnCount.y += effect.corridorTrapsIncrease;
        effect.specialEffect?.Apply();
    }
}

[Serializable][FoldoutGroup("Ascension Effect")]
public class AscensionEffect
{
    public float gameTimeMinutesReduction;
    public int inventorySizeReduction;
    public float trapChanceIncrease;
    public Damage startDamage;
    public int corridorTrapsIncrease;
    public AscensionEffectSpecial specialEffect;
}

public abstract class AscensionEffectSpecial : MonoBehaviour
{
    public abstract void Apply();
}
