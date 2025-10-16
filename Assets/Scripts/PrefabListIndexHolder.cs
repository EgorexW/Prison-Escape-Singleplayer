using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class PrefabListIndexHolder : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PrefabList prefabList;
    
    public int prefabListIndex;

    void OnValidate()
    {
        prefabListIndex = prefabList.GetPrefabIndex(gameObject);
    }
}