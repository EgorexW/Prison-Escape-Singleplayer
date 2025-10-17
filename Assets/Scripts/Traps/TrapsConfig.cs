using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

class TrapsConfig : MonoBehaviour
{
    [Range(0, 2)] public float trapNrMod = 1;
    [SerializeField] SpawnTable spawnTable;

    public GameObject GetTrapPrefab()
    {
        return spawnTable.GetGameObject();
    }
}