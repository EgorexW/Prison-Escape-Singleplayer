using System.Collections.Generic;
using UnityEngine;

public static class WeightedChance
{
    public static ObjectWithValue<T> GetWeightedRoll<T>(List<ObjectWithValue<T>> weightedChances)
    {
        ObjectWithValue<T> win = null;
        float totalWeight = 0;

        foreach(ObjectWithValue<T> weightedChance in weightedChances)
        {
            totalWeight += Mathf.Max(weightedChance.value, 0);
        }

        float roll = Random.Range(0, totalWeight);

        foreach(ObjectWithValue<T> weightedChance in weightedChances)
        {
            if (roll <= weightedChance.value)
            {
                win = weightedChance;
                break;
            }
            roll -= weightedChance.value;
        }

        return win;
    }
}
