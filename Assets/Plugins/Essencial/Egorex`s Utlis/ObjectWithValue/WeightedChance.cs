using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeightedChance
{
    public static ObjectWithValue<T> GetWeightedRoll<T>(ObjectWithValue<T>[] weightedChances)
    {
        ObjectWithValue<T> win = null;
        float totalWeight = 0;

        foreach(ObjectWithValue<T> weightedChance in weightedChances)
        {
            totalWeight += weightedChance.value;
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
