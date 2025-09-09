using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WorldInit : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] LevelNodes levelNodes;
    [BoxGroup("References")] [Required] [SerializeField] RoomGenerator roomGenerator;
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    [BoxGroup("References")] [SerializeField] List<CorridorSpawner> corridorSpawners;

    [SerializeField] float spawnDelay = 1f;

    void Awake()
    {
        General.CallAfterSeconds(Generate, spawnDelay);
    }

    public void Generate()
    {
        roomGenerator.Generate();
        var spawn = FindAnyObjectByType<PlayerSpawn>();
        spawn.Spawn(player);
        foreach (var corridorSpawner in corridorSpawners) corridorSpawner.Spawn(levelNodes.CorridorNodes);
    }
}