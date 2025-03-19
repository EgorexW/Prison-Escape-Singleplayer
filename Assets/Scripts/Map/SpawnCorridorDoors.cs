using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnCorridorDoors : MonoBehaviour
{
    [SerializeField] float initDelay = 1;
    
    [Required] [SerializeField] LevelNodes nodes;
    
    [Required][SerializeField] SpawnTable spawnTable;

    [SerializeField] Vector2Int doorSpawns = new(5, 10);

    void Start()
    {
        General.CallAfterSeconds(Spawn, initDelay);
    }

    void Spawn()
    {
        var possibleSpawns = GetPossibleSpawns();
        var spawnsLeft = General.RandomRange(doorSpawns);
        while (spawnsLeft > 0){
            var choosenSpawn = possibleSpawns.Random(true);
            var door = Instantiate(spawnTable.GetGameObject(), choosenSpawn.pos, choosenSpawn.rotation, transform);
            spawnsLeft--;
        }
    }

    List<PossibleSpawn> GetPossibleSpawns()
    {
        var possibleSpawns = new HashSet<PossibleSpawn>();
        foreach (var node in nodes.CorridorNodes){
            foreach (var connection in node.SameTypeConnections){
                PossibleSpawn possibleSpawn = new PossibleSpawn{
                    pos = node.transform.position + connection,
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
