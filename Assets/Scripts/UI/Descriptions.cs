using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using TMPro;
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
        if (effect.maxTrapAmountIncrease > 0){
            lines.Add("More traps can spawn inside the same room");
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

    public static string GetStatsDescription(Stats stats)
    {
        var lines = new List<string>{
            $"Floor Nr: {Ascensions.AscensionLevel}",
            $"Game Time: {TimeSpan.FromSeconds(stats.gameTime):hh\\:mm\\:ss}",
            $"Light Damage Taken: {Mathf.Round(stats.normalDamageTaken)}",
            $"Heavy Damage Taken: {Mathf.Round(stats.pernamentDamageTaken)}",
            $"Meters Walked: {Mathf.Round(stats.metersWalked)}",
            $"Unique Rooms Entered: {stats.uniqueRoomsEntered}",
            $"Objects Destroyed: {stats.objectsDestroyed}",
            $"Unique Items Picked Up: {stats.uniqueItemsPickedUp}"
        };
        return string.Join("\n", lines);
    }

    public static string GetTaskDescription(Task task)
    {
        var lines = new List<string>();
        if (task.normalDamageToTake > 0){
            lines.Add($"Take {task.normalDamageToTake} light damage");
        }
        if (task.pernamentDamageToTake > 0){
            lines.Add($"Take {task.pernamentDamageToTake} heavy damage");
        }
        if (task.uniqueRoomsToEnter > 0){
            lines.Add($"Enter {task.uniqueRoomsToEnter} unique rooms");
        }
        if (task.objectsToDestroy > 0){
            lines.Add($"Destroy {task.objectsToDestroy} objects");
        }
        if (task.uniqueItemsToPickUp > 0){
            lines.Add($"Pick up {task.uniqueItemsToPickUp} unique items");
        }
        return string.Join(",\n ", lines);
    }
}