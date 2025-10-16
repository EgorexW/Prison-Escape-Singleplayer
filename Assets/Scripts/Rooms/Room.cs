using System;
using System.Collections.Generic;
using System.Linq;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] RoomSpawnConfig roomSpawnConfig;
    [BoxGroup("References")] [Required] [SerializeField] GameObject lootSpawnersParent;
    
    [SerializeField] public DoorwayConfig doorway;
    [SerializeField] public RoomTraps roomTraps;

    [SerializeField] List<GameObject> gameObjectsToActivate;

    List<LootSpawner> LootSpawners => new(lootSpawnersParent.GetComponentsInChildren<LootSpawner>());
    
    [SerializeField] public RoomTrait[] traits;
    
    [FoldoutGroup("Events")] public UnityEvent onActivate;
    
    public bool discovered;
    public string roomName;

    public void Spawn(Vector3 position, Vector3 dir)
    {
        roomSpawnConfig.Spawn(position, dir);
            foreach (var obj in gameObjectsToActivate){
                obj.SetActive(false);
            }
    }

    public void Activate()
    {
        SpawnLoot();
        roomTraps?.Activate();
        foreach (var obj in gameObjectsToActivate){
            obj.SetActive(true);
        }
        onActivate?.Invoke();
    }

    void SpawnLoot()
    {
        foreach (var spawner in LootSpawners){
            spawner.SpawnGameObjects();
        }
    }

    void OnValidate()
    {
        if (roomName.IsNullOrWhitespace()){
            CopyGameobjectName();
        }
    }

    [Button]
    void CopyGameobjectName()
    {
        gameObject.name = roomName;
    }
}