using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class AISpawner : MonoBehaviour
{
    [SerializeField] SpawnTable spawnTable;
    [SerializeField] Vector2Int spawnCount = new Vector2Int(5, 10);
    
    [BoxGroup("Spawn Conditions")][SerializeField] CorridorNodeType nodeTypesAllowed;
    // [BoxGroup("Spawn Conditions")] [SerializeField] bool alignWithConnections = true;
    
    public void Spawn(List<LevelNode> nodes, MainAI mainAI)
    {
        var spawnNr = Random.Range(spawnCount.x, spawnCount.y);
        while (true){
            if (nodes.Count == 0) break;
            var node = nodes.Random();
            nodes.Remove(node);
            if (!nodeTypesAllowed.HasFlag(node.CorridorNodeType)) continue;
            var rotation = Quaternion.LookRotation(node.Directions.Random());
            var obj = Instantiate(spawnTable.GetGameObject(), node.transform.position,  rotation, transform);
            var aiObject = obj.GetComponent<IAIObject>();
            if (aiObject != null){
                mainAI.AddObject(aiObject);
            }
            spawnNr--;
            if (spawnNr == 0) break;
        }
    }
}