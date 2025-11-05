using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PrefabListIndexHolder : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PrefabList prefabList;
    
    public int prefabListIndex;

    [Button]
    void SaveIndex()
    {
        // AddToPrefabList();
        prefabListIndex = prefabList.GetPrefabIndex(gameObject);
    }

//     void AddToPrefabList()
//     {
//         if (!prefabList.prefabs.Contains(gameObject))
//         {
//             prefabList.prefabs.Add(gameObject);
//
// #if UNITY_EDITOR
//             EditorUtility.SetDirty(prefabList);
//             AssetDatabase.SaveAssets();
// #endif
//         }
//     }
}