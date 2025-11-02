using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Ascensions : MonoBehaviour
{
    [ShowInInspector] public static int AscensionLevel;

    public List<GameModifier> effects;
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
        GameManager.i.gameModifiers.ApplyEffects(GetActiveEffects());
    }

    public List<GameModifier> GetActiveEffects()
    {
        var list = new List<GameModifier>();
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