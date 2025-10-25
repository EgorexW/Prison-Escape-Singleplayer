using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] SpawnTable spawnTable;
    [SerializeField] [MinMaxSlider(0, "@GetSpawnPoints().Count")] Vector2Int spawnNr = new(1, 1);
    [SerializeField] bool randomRotation = true;
    [SerializeField] bool spawnOnStart;

    void Start()
    {
        if (spawnOnStart){
            SpawnGameObjects();
        }
    }

    List<Transform> GetSpawnPoints()
    {
        var points = new List<Transform>(GetComponentsInChildren<Transform>());
        points.Remove(transform);
        return points;
    }

    public void SpawnGameObjects()
    {
        if (spawnTable == null){
            Debug.LogWarning("SpawnTable is null", this);
            return;
        }
        var roll = General.RandomRange(spawnNr);
        roll = Mathf.Min(roll, GetSpawnPoints().Count);
        var availibleSpawnPoints = GetSpawnPoints();
        for (var i = 0; i < roll; i++){
            var spawnedObject = spawnTable.GetGameObject();
            var spawnPoint = availibleSpawnPoints.Random();
            availibleSpawnPoints.Remove(spawnPoint);
            if (spawnedObject == null){
                Debug.LogWarning("GameObject is null", this);
                return;
            }
            var rotation = transform.rotation;
            if (randomRotation){
                rotation = Random.rotationUniform;
            }
            Instantiate(spawnedObject, spawnPoint.position, rotation, transform);
        }
    }
}