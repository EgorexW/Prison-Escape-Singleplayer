using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class GameInit : MonoBehaviour
{
    const float DELAY = 0.5f;
    [BoxGroup("References")] [Required] [SerializeField] LevelNodes levelNodes;
    [BoxGroup("References")] [Required] [SerializeField] RoomGenerator roomGenerator;
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    [BoxGroup("References")] [SerializeField] List<CorridorSpawner> corridorSpawners;

    [FoldoutGroup("Events")] public UnityEvent onFinish;

    IEnumerator Start()
    {
        Debug.Log("GameInit Start, starting generation coroutine", this);
        yield return GenerateRoutine();
    }

    IEnumerator GenerateRoutine()
    {
        yield return new WaitForSeconds(DELAY);
        var rooms = roomGenerator.GenerateRooms();
        Debug.Log("Room generated, waiting to generate corridors", this);

        yield return new WaitForSeconds(DELAY);
        foreach (var room in rooms){
            room.Activate();
        }
        levelNodes.ResetNodes();

        foreach (var corridorSpawner in corridorSpawners)
            corridorSpawner.Spawn(levelNodes.CorridorNodes);

        PlayerSpawn spawn = null;
        while (spawn == null)
        {
            spawn = FindAnyObjectByType<PlayerSpawn>();
            if (spawn != null){
                continue;
            }
            Debug.LogWarning("No PlayerSpawn found in scene, retrying...", this);
            yield return null;
        }

        spawn.Spawn(player);
        levelNodes.ResetNodes();
        onFinish.Invoke();
        GameManager.i.gameTimeManager.startTime = Time.time;

        Debug.Log("Corridors generated, player spawned, game init finished", this);
    }
}