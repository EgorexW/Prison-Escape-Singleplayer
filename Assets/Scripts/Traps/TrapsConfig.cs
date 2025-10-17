using System.Collections.Generic;
using UnityEngine;

class TrapsConfig : MonoBehaviour
{
    [Range(0, 1)] public float trapChance = 0.5f;
    [SerializeField] SpawnTable spawnTable;

    public GameObject GetTrapPrefab()
    {
        return spawnTable.GetGameObject();
    }
}