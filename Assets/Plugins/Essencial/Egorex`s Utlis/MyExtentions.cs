using System.Collections.Generic;
using UnityEngine;

public static class MyExtentions{
    public static void Shuffle<T>(this List<T> list){
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }
    public static T Random<T>(this List<T> list){
        if (list.Count < 1){
            return default;
        }
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
    public static T WeightedRandom<T>(this Dictionary<T, int> list){
        T win = default;
        float totalWeight = 0;

        foreach(var weightedChance in list)
        {
            totalWeight += Mathf.Max(weightedChance.Value, 0);
        }

        float roll = UnityEngine.Random.Range(0, totalWeight);

        foreach(var weightedChance in list)
        {
            if (roll <= weightedChance.Value)
            {
                win = weightedChance.Key;
                break;
            }
            roll -= weightedChance.Value;
        }

        return win;
    }
    public static List<T> Copy<T>(this List<T> list){
        return new List<T>(list);
    }
}