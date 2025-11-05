using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Prefab List", fileName = "Prefab List", order = 0)]
public class PrefabList : ScriptableObject
{
    public List<GameObject> prefabs;

    public int GetPrefabIndex(GameObject prefab)
    {
        for (int i = 0; i < prefabs.Count; i++){
            if (prefab.name == prefabs[i].name){
                return i;
            }
        }
        Debug.LogWarning("Didn't find prefab", prefab);
        return -1;
    }
}
