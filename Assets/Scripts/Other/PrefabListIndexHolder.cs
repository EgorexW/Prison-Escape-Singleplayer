using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class PrefabListIndexHolder : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PrefabList prefabList;
    
    public int prefabListIndex;

    [Button]
    void SaveIndex()
    {
        prefabListIndex = prefabList.GetPrefabIndex(gameObject);
    }
}