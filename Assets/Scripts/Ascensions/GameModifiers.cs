using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameModifiers : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] CorridorSpawner corridorTrapsSpawner;

    List<GameModifier> activeModifiers = new List<GameModifier>();
    
    void ApplyEffect(GameModifier effect)
    {
        GameManager.i.gameTimeManager.gameTimeMinutes -= effect.gameTimeMinutesReduction;
        GameManager.i.Player.GetInventory()
            .SetSize(GameManager.i.Player.GetInventory().GetSize() - effect.inventorySizeReduction);
        GameManager.i.trapsManager.trapChance += effect.trapChanceIncrease;
        GameManager.i.Player.playerHealth.Damage(effect.startDamage);
        corridorTrapsSpawner.spawnCount.x += effect.corridorTrapsIncrease;
        corridorTrapsSpawner.spawnCount.y += effect.corridorTrapsIncrease;
        effect.specialEffect?.Apply();
        activeModifiers.Add(effect);
        Debug.Log($"Game Modifier applied: {effect.GetEffectDescription()}");
    }

    public void ApplyEffects(List<GameModifier> gameModifiers)
    {
        foreach (var effect in gameModifiers){
            ApplyEffect(effect);
        }
    }

    public List<GameModifier> GetActiveModifiers()
    {
        return activeModifiers.Copy();
    }
}

[Serializable]
[FoldoutGroup("Game Modifier")]
public class GameModifier
{
    public float gameTimeMinutesReduction;
    public int inventorySizeReduction;
    public float trapChanceIncrease;
    public Damage startDamage;
    public int corridorTrapsIncrease;
    public GameModifierSpecial specialEffect;
}

public abstract class GameModifierSpecial : MonoBehaviour
{
    [SerializeField] string effectDescription;

    public abstract void Apply();

    public virtual string GetEffectDescription()
    {
        return effectDescription;
    }
}