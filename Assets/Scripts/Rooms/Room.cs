using System;
using System.Collections.Generic;
using System.Linq;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] RoomSpawnConfig roomSpawnConfig;
    [SerializeField] public DoorwayConfig doorway;
    
    [SerializeField] List<LootSpawner> lootSpawners;
    
    [SerializeField] public RoomTrait[] traits;
    
    [FoldoutGroup("Events")] public UnityEvent onActivate;
    
    public bool discovered;
    public string Name => doorway.roomName;

    public void Spawn(Vector3 position, Vector3 dir)
    {
        roomSpawnConfig.Spawn(position, dir);
    }

    public void Activate()
    {
        SpawnLoot();
        onActivate?.Invoke();
    }

    void SpawnLoot()
    {
        var foundLootSpawners = GetComponentsInChildren<LootSpawner>().ToList();
        if (lootSpawners.Count != foundLootSpawners.Count){
            Debug.LogWarning("Found different amount of LootSpawners than assigned in inspector, updating list", this);
            lootSpawners = foundLootSpawners;
        }
        foreach (var spawner in lootSpawners){
            spawner.SpawnGameObjects();
        }
    }

    void Reset()
    {
        lootSpawners = GetComponentsInChildren<LootSpawner>().ToList();
    }
}