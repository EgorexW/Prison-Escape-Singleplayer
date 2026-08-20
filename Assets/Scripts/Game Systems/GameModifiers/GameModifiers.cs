using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameModifiers : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] CorridorSpawner corridorTrapsSpawner;

    List<GameModifier> activeModifiers = new List<GameModifier>();
    
    void ApplyEffectBeforeInit(GameModifier effect)
    {
        GameManager.i.gameTimeManager.gameTimeMinutes -= effect.gameTimeMinutesReduction;
        GameManager.i.Player.GetInventory()
            .SetSize(GameManager.i.Player.GetInventory().GetSize() - effect.inventorySizeReduction);
        GameManager.i.trapsManager.trapChance += effect.trapChanceIncrease;
        GameManager.i.trapsManager.maxTrapAmount += effect.maxTrapAmountIncrease;
        GameManager.i.Player.playerHealth.Damage(effect.startDamage);
        corridorTrapsSpawner.spawnCount.x += effect.corridorTrapsIncrease;
        corridorTrapsSpawner.spawnCount.y += effect.corridorTrapsIncrease;
        effect.specialEffect?.ApplyBeforeInit();
        // Debug.Log($"Game Modifier applied: {effect.GetEffectDescription()}");
    }

    void ApplyEffectAfterInit(GameModifier effect)
    {
        effect.specialEffect?.ApplyAfterInit();
    }
    
    public void ApplyEffectsBeforeInit()
    {
        foreach (var effect in activeModifiers){
            ApplyEffectBeforeInit(effect);
        }
    }

    public List<GameModifier> GetActiveModifiers()
    {
        return activeModifiers.Copy();
    }

    public void AddEffects(List<GameModifier> effects)
    {
        foreach (var effect in effects){
            AddEffect(effect);
        }
    }

    void AddEffect(GameModifier effect)
    {
        activeModifiers.Add(effect);
    }

    public void ApplyEffectsAfterInit()
    {
        foreach (var effect in activeModifiers){
            ApplyEffectAfterInit(effect);
        }
    }

}

[Serializable]
[FoldoutGroup("Game Modifier")]
public class GameModifier
{
    public float gameTimeMinutesReduction;
    public int inventorySizeReduction;
    public float trapChanceIncrease;
    public int maxTrapAmountIncrease;
    public Damage startDamage;
    public int corridorTrapsIncrease;
    public GameModifierSpecial specialEffect;
}

public abstract class GameModifierSpecial : MonoBehaviour
{
    [SerializeField] string effectDescription;

    public virtual void ApplyBeforeInit()
    {
        
    }

    public virtual void ApplyAfterInit()
    {
        
    }

    public virtual string GetEffectDescription()
    {
        return effectDescription;
    }
}