using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CorridorSpawner : MonoBehaviour
{
    [SerializeField] SpawnTable spawnTable;
    [SerializeField] public Vector2Int spawnCount = new(5, 10);

    [BoxGroup("Spawn Conditions")] [SerializeField] CorridorNodeType nodeTypesAllowed;
    [BoxGroup("Spawn Conditions")] [SerializeField] bool placeOnConnection;

    public void Spawn(List<CorridorNode> levelNodes)
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
            var connection = node.SameTypeConnections().Random();
            var rotation = Quaternion.LookRotation(connection.normalized);
            var spawnPosition = node.transform.position;
            if (placeOnConnection){
                spawnPosition += connection / 2;
            }
            var obj = Instantiate(spawnTable.GetGameObject(), spawnPosition, rotation, transform);
            spawnNr--;
            if (spawnNr == 0){
                break;
            }
        }
    }
}