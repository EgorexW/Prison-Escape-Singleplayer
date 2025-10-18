using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class GameInit : MonoBehaviour
{
    const float DELAY = 0.5f;
    [BoxGroup("References")] [Required] [SerializeField] RoomGenerator roomGenerator;
    [BoxGroup("References")] [SerializeField] List<CorridorSpawner> corridorSpawners;

    [FoldoutGroup("Events")] public UnityEvent onFinish;

    IEnumerator Start()
    {
        // Debug.Log("GameInit Start, starting generation coroutine", this);
        yield return GenerateRoutine();
    }

    IEnumerator GenerateRoutine()
    {
        GameManager.i.ascensions.SetupAscensions();
        yield return new WaitForSeconds(DELAY);
        var rooms = roomGenerator.GenerateRooms();
        // Debug.Log("Room generated, waiting to generate corridors", this);

        yield return new WaitForSeconds(DELAY);
        foreach (var room in rooms){
            room.Activate();
        }
        GameManager.i.levelNodes.ResetNodes();

        foreach (var corridorSpawner in corridorSpawners)
            corridorSpawner.Spawn(GameManager.i.levelNodes.CorridorNodes);

        yield return SpawnPlayer();
        
        GameManager.i.levelNodes.ResetNodes();
        onFinish.Invoke();
        GameManager.i.gameTimeManager.StartGame();

        // Debug.Log("Corridors generated, player spawned, game init finished", this);
    }

    IEnumerator SpawnPlayer()
    {
        PlayerSpawn spawn = null;
        while (spawn == null){
            var spawns = FindObjectsByType<PlayerSpawn>(FindObjectsSortMode.None);
            foreach (var playerSpawn in spawns){
                if (!playerSpawn.CanSpawn()){
                    continue;
                }
                spawn = playerSpawn;
                break;
            }
            if (spawn != null){
                continue;
            }
            Debug.LogWarning("No PlayerSpawn can spawn the player, retrying...", this);
            yield return null;
        }

        spawn.Spawn(GameManager.i.Player);
    }
}