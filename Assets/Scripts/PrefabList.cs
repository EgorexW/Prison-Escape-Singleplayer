using UnityEngine;

public class PrefabList : MonoBehaviour
{
    public GameObject[] prefabs;

    public int GetPrefabIndex(GameObject prefab)
    {
        for (int i = 0; i < prefabs.Length; i++){
            if (prefab == prefabs[i]){
                return i;
            }
        }
        Debug.LogWarning("Didn't find prefab", prefab);
        return -1;
    }
}
