using UnityEngine;


[CreateAssetMenu(menuName = "Prefab List", fileName = "Prefab List", order = 0)]
public class PrefabList : ScriptableObject
{
    public GameObject[] prefabs;

    public int GetPrefabIndex(GameObject prefab)
    {
        for (int i = 0; i < prefabs.Length; i++){
            if (prefab.name == prefabs[i].name){
                return i;
            }
        }
        Debug.LogWarning("Didn't find prefab", prefab);
        return -1;
    }
}
