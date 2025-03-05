using System.Collections.Generic;

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
    public static List<T> Copy<T>(this List<T> list){
        return new List<T>(list);
    }
}