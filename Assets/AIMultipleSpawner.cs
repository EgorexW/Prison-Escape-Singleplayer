using System.Collections.Generic;
using UnityEngine;

public class AIMultipleSpawner : MonoBehaviour
{
    public SpawnTable spawnTable;
    public Vector2Int spawnCount = new Vector2Int(5, 10);
    
    public void Spawn(List<AINode> nodes, MainAI mainAI)
    {
        var spawnNr = Random.Range(spawnCount.x, spawnCount.y);
        for (int i = 0; i < spawnNr; i++){
            var node = nodes.Random();
            nodes.Remove(node);
            var obj = Instantiate(spawnTable.GetGameObject(), node.transform.position,  Quaternion.LookRotation(General.Get4MainDirections3D().Random()), transform);
            mainAI.AddObject(obj.GetComponent<IAIObject>());
            if (nodes.Count == 0) break;
        }
    }
}