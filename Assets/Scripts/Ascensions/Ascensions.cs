using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Ascensions : MonoBehaviour
{
    const int MAX_UNLOCKED_ASCENSION = 7;
    
    public List<AscensionEffect> effects;

    [BoxGroup("References")] [Required] [SerializeField] CorridorSpawner corridorTrapsSpawner;

    [ShowInInspector] public static int AscensionLevel = 0;

    void Start()
    {
        GameManager.i.gameEnder.beforeWinGame.AddListener(OnWin);
    }

    void OnWin()
    {
        if (GetUnlockedAscension() < AscensionLevel){
            Debug.Log($"Ascension {AscensionLevel} unlocked");
            PlayerPrefs.SetInt(PlayerPrefsKeys.UnlockedAscension, Mathf.Min(AscensionLevel, MAX_UNLOCKED_ASCENSION));
        }
    }

    public static int GetUnlockedAscension()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.UnlockedAscension, 0);
    }

    public void SetupAscensions()
    {
        foreach (var effect in GetActiveEffects()){
            ApplyEffect(effect);
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
        Debug.Log($"Ascension Effect applied: {effect.GetEffectDescription()}");
    }

    public List<AscensionEffect> GetActiveEffects()
    {
        var list = new List<AscensionEffect>();
        for (int i = 0; i <= AscensionLevel - 1; i++){
            if (effects.Count <= i){
                Debug.LogWarning("Run out of ascensions", this);
                break;
            }
            list.Add(effects[i]);
        }
        return list;
    }

    public static void SetAscensionLevel(int newLevel)
    {
        AscensionLevel = newLevel;
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
    [SerializeField] string effectDescription;
    
    public abstract void Apply();

    public virtual string GetEffectDescription()
    {
        return effectDescription;
    }
}
