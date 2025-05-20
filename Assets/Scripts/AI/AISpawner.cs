using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AISpawner : MonoBehaviour, IAISpawner
{
    [SerializeField] SpawnTable spawnTable;
    [SerializeField] Vector2Int spawnCount = new(5, 10);

    [BoxGroup("Spawn Conditions")] [SerializeField] CorridorNodeType nodeTypesAllowed;
    // [BoxGroup("Spawn Conditions")] [SerializeField] bool alignWithConnections = true;

    public void Spawn(List<LevelNode> levelNodes, AIDirector aiDirector)
    {
        var spawnNr = Random.Range(spawnCount.x, spawnCount.y);
        while (true){
            if (levelNodes.Count == 0){
                Debug.LogWarning($"No more nodes to spawn on, spawns left: {spawnNr}", this);
                break;
            }
            var node = levelNodes.Random();
            levelNodes.Remove(node);
            if (!nodeTypesAllowed.HasFlag(node.CorridorNodeType)){
                continue;
            }
            var rotation = Quaternion.LookRotation(node.Directions.Random());
            var obj = Instantiate(spawnTable.GetGameObject(), node.transform.position, rotation, transform);
            var aiObject = obj.GetComponentInChildren<IAIObject>();
            if (aiObject != null){
                aiDirector.AddObject(aiObject);
            }
            spawnNr--;
            if (spawnNr == 0){
                break;
            }
        }
    }
}