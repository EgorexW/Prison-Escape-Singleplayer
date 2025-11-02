using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public static class Descriptions
{
    public static string GetEffectDescription(this GameModifier effect)
    {
        var lines = new List<string>();
        if (effect.gameTimeMinutesReduction > 0){
            lines.Add("Time reduction");
        }
        if (effect.inventorySizeReduction > 0){
            lines.Add("Lower item capacity");
        }
        if (effect.trapChanceIncrease > 0){
            if (effect.trapChanceIncrease > 1){
                lines.Add("Way more traps inside rooms");
            }
            else{
                lines.Add("More traps inside rooms");
            }
        }
        if (!effect.startDamage.IsZero){
            lines.Add("Start damaged");
        }
        if (effect.corridorTrapsIncrease > 0){
            lines.Add("More obstacles in the corridors");
        }
        if (effect.specialEffect != null){
            var effectDescription = effect.specialEffect.GetEffectDescription();
            if (!effectDescription.IsNullOrWhitespace()){
                lines.Add(effectDescription);
            }
        }
        return string.Join(", ", lines);
    }

    public static string GetStatsDescription(this GameStats stats)
    {
        var lines = new List<string>{
            $"Floor Nr: {Ascensions.AscensionLevel}",
            $"Game Time: {TimeSpan.FromSeconds(stats.gameTime):hh\\:mm\\:ss}",
            $"Light Damage Taken: {Mathf.Round(stats.normalDamageTaken)}",
            $"Heavy Damage Taken: {Mathf.Round(stats.pernamentDamageTaken)}",
            $"Meters Walked: {Mathf.Round(stats.metersWalked)}",
            $"Unique Rooms Entered: {stats.uniqueRoomsEntered}"
        };
        return string.Join("\n", lines);
    }
}