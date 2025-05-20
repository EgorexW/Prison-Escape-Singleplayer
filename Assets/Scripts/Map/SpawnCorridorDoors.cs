using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnCorridorDoors : MonoBehaviour, IAISpawner
{
    [Required][SerializeField] SpawnTable spawnTable;

    [SerializeField] Vector2Int doorSpawns = new(5, 10);

    public void Spawn(List<LevelNode> levelNodes, AIDirector aiDirector)
    {
        var possibleSpawns = GetPossibleSpawns(levelNodes);
        // Debug.Log("Possible spawns: " + possibleSpawns.Count);
        var spawnsLeft = General.RandomRange(doorSpawns);
        if (spawnsLeft > possibleSpawns.Count){
            Debug.LogWarning($"No more nodes to spawn on, spawns left: {spawnsLeft - possibleSpawns.Count}", this);
            spawnsLeft = possibleSpawns.Count;
        }
        while (spawnsLeft > 0){
            var choosenSpawn = possibleSpawns.Random(true);
            var door = Instantiate(spawnTable.GetGameObject(), choosenSpawn.pos, choosenSpawn.rotation, transform);
            var aiObject = door.GetComponentInChildren<IAIObject>();
            if (aiObject != null){
                aiDirector.AddObject(aiObject);
            }
            spawnsLeft--;
        }
    }

    List<PossibleSpawn> GetPossibleSpawns(List<LevelNode> levelNodes)
    {
        var possibleSpawns = new List<PossibleSpawn>();
        // Debug.Log("Level nodes: " + levelNodes.Count);
        foreach (var node in levelNodes){
            foreach (var connection in node.SameTypeConnections){
                var possibleSpawn = new PossibleSpawn{
                    pos = node.transform.position + connection/2,
                    rotation = Quaternion.LookRotation(connection.normalized)
                };
                if (possibleSpawns.Any(x => Vector3.Distance(x.pos, possibleSpawn.pos) < 0.5f)){
                    continue;
                }
                possibleSpawns.Add(possibleSpawn);
            }
        }
        return possibleSpawns;
    }
}

struct PossibleSpawn
{
    public Vector3 pos;
    public Quaternion rotation;
}
