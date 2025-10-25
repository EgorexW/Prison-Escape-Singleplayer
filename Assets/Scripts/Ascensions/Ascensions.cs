using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Ascensions : MonoBehaviour
{
    [ShowInInspector] public static int AscensionLevel;

    public List<AscensionEffect> effects;

    [BoxGroup("References")] [Required] [SerializeField] CorridorSpawner corridorTrapsSpawner;
    int maxUnlockedAscension => effects.Count;

    void Start()
    {
        GameManager.i.gameEnder.beforeWinGame.AddListener(OnWin);
    }

    void OnWin()
    {
        var unlockedAscension = Mathf.Min(AscensionLevel, maxUnlockedAscension);
        if (GetUnlockedAscension() >= unlockedAscension){
            return;
        }
        Debug.Log($"Ascension {unlockedAscension} unlocked");
        PlayerPrefs.SetInt(PlayerPrefsKeys.UnlockedAscension, unlockedAscension);
    }

    public static int GetUnlockedAscension()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.UnlockedAscension, 0);
    }

    public void SetupAscensions()
    {
        foreach (var effect in GetActiveEffects()) ApplyEffect(effect);
    }

    void ApplyEffect(AscensionEffect effect)
    {
        GameManager.i.gameTimeManager.gameTimeMinutes -= effect.gameTimeMinutesReduction;
        GameManager.i.Player.GetInventory()
            .SetSize(GameManager.i.Player.GetInventory().GetSize() - effect.inventorySizeReduction);
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
        for (var i = 0; i <= AscensionLevel - 1; i++){
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

[Serializable]
[FoldoutGroup("Ascension Effect")]
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