using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class GameInit : MonoBehaviour
{
    const float DELAY = 1f;
    [BoxGroup("References")] [Required] [SerializeField] LevelNodes levelNodes;
    [BoxGroup("References")] [Required] [SerializeField] RoomGenerator roomGenerator;
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    [BoxGroup("References")] [SerializeField] List<CorridorSpawner> corridorSpawners;

    [FoldoutGroup("Events")] public UnityEvent onFinish;

    void Awake()
    {
        Invoke(nameof(Generate), DELAY);
        Debug.Log("GameInit Awake, will generate in " + DELAY + " seconds", this);
    }

    public void Generate()
    {
        roomGenerator.Generate();
        Invoke(nameof(Generate2), DELAY);
        Debug.Log("Room generated, will generate corridors in " + DELAY + " seconds", this);
    }

    void Generate2()
    {
        levelNodes.ResetNodes();
        foreach (var corridorSpawner in corridorSpawners) corridorSpawner.Spawn(levelNodes.CorridorNodes);
        var spawn = FindAnyObjectByType<PlayerSpawn>();
        if (spawn == null){
            Debug.LogWarning("No PlayerSpawn found in scene", this);
            Invoke(nameof(Generate2), DELAY);
            return;
        }
        spawn.Spawn(player);
        levelNodes.ResetNodes();
        onFinish.Invoke();
        GameManager.i.gameTimeManager.startTime = Time.time;
        Debug.Log("Corridors generated, player spawned, game init finished", this);
    }
}