
using System;
using System.Collections.Generic;
using UnityEngine;

public static class Descriptions
    {
        public static string GetEffectDescription(this AscensionEffect effect)
        {
            var lines = new List<string>();
            if (effect.gameTimeMinutesReduction > 0){
                lines.Add("Time reduction");
            }
            if (effect.inventorySizeReduction > 0){
                lines.Add("Lower item capacity");
            }
            if (effect.trapChanceIncrease > 0){
                lines.Add("More traps inside rooms");
            }
            if (!effect.startDamage.IsZero){
                lines.Add("Start damaged");
            }
            if (effect.corridorTrapsIncrease > 0){
                lines.Add("More obstacles in the corridors");
            }
            if (effect.specialEffect != null){
                lines.Add(effect.specialEffect.GetEffectDescription());
            }
            return string.Join(", ", lines);
        }

        public static string GetStatsDescription(this GameStats stats)
        {
            var lines = new List<string>();
            lines.Add($"Floor Nr: {Ascensions.AscensionLevel}");
            lines.Add($"Game Time: {TimeSpan.FromSeconds(stats.gameTime):hh\\:mm\\:ss}");
            lines.Add($"Light Damage Taken: {Mathf.Round(stats.normalDamageTaken)}");
            lines.Add($"Heavy Damage Taken: {Mathf.Round(stats.pernamentDamageTaken)}");
            lines.Add($"Meters Walked: {Mathf.Round(stats.metersWalked)}");
            lines.Add($"Unique Rooms Entered: {stats.uniqueRoomsEntered}");
            return string.Join("\n", lines);
        }
    }