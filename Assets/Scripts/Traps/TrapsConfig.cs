using System.Collections.Generic;
using UnityEngine;

class TrapsConfig : MonoBehaviour
{
    [Range(0, 1)] public float trapChance = 0.3f;
    [SerializeField] List<GameObject> trapPrefabs;

    public GameObject GetTrapPrefab()
    {
        return trapPrefabs.Random();
    }
}