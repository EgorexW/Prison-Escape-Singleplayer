using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnCorridorDoors : MonoBehaviour, IAISpawner
{
    [Required][SerializeField] SpawnTable spawnTable;

    [SerializeField] Vector2Int doorSpawns = new(5, 10);

    public void Spawn(List<LevelNode> levelNodes, MainAI mainAI)
    {
        var possibleSpawns = GetPossibleSpawns(levelNodes);
        var spawnsLeft = General.RandomRange(doorSpawns);
        while (spawnsLeft > 0){
            var choosenSpawn = possibleSpawns.Random(true);
            var door = Instantiate(spawnTable.GetGameObject(), choosenSpawn.pos, choosenSpawn.rotation, transform);
            var aiObject = door.GetComponentInChildren<IAIObject>();
            if (aiObject != null){
                mainAI.AddObject(aiObject);
            }
            spawnsLeft--;
        }
    }

    List<PossibleSpawn> GetPossibleSpawns(List<LevelNode> levelNodes)
    {
        var possibleSpawns = new HashSet<PossibleSpawn>();
        foreach (var node in levelNodes){
            foreach (var connection in node.SameTypeConnections){
                PossibleSpawn possibleSpawn = new PossibleSpawn{
                    pos = node.transform.position + connection/2,
                    rotation = Quaternion.LookRotation(connection.normalized)
                };
                possibleSpawns.Add(possibleSpawn);
            }
        }
        return possibleSpawns.ToList();
    }
}

struct PossibleSpawn
{
    public Vector3 pos;
    public Quaternion rotation;
}
