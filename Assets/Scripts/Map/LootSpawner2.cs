using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class LootSpawner2 : MonoBehaviour
{ 
    [SerializeField] SpawnTable spawnTable;
    [SerializeField] [MinMaxSlider(0, "@SpawnPoints.Count")] Vector2Int spawnNr = new Vector2Int(1, 1);
    [SerializeField] bool randomRotation = true;
    
    List<Transform> SpawnPoints
    {
        get
        {
            var points = new List<Transform>(GetComponentsInChildren<Transform>());
            points.Remove(this.transform);
            return points;
        }
    }

    void Awake(){
        SpawnGameObjects();
    }

    protected void SpawnGameObjects()
    {
        if (spawnTable == null){
            Debug.LogWarning("SpawnTable is null", this);
            return;
        }
        var roll = General.RandomRange(spawnNr);
        roll = Mathf.Min(roll, SpawnPoints.Count);
        var availibleSpawnPoints = SpawnPoints;
        for (int i = 0; i < roll; i++)
        {
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
        Destroy(this.gameObject);
    }
}
