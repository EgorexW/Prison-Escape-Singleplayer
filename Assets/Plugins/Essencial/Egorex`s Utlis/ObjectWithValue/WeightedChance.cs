using System.Collections.Generic;
using UnityEngine;

public static class WeightedChance
{
    public static ObjectWithValue<T> GetWeightedRoll<T>(List<ObjectWithValue<T>> weightedChances)
    {
        ObjectWithValue<T> win = null;
        float totalWeight = 0;

        foreach (var weightedChance in weightedChances) totalWeight += Mathf.Max(weightedChance.value, 0);

        var roll = Random.Range(0, totalWeight);

        foreach (var weightedChance in weightedChances){
            if (roll <= weightedChance.value){
                win = weightedChance;
                break;
            }
            roll -= weightedChance.value;
        }
        
        if (win == null){
            Debug.LogError("WeightedChance: No winner was selected. Check if the total weight is greater than 0.");
        }
        
        return win;
    }
}