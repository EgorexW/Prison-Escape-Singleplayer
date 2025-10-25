using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] RoomSpawnConfig roomSpawnConfig;
    [BoxGroup("References")] [Required] [SerializeField] GameObject lootSpawnersParent;

    [SerializeField] public DoorwayConfig doorway;
    [SerializeField] public RoomTraps roomTraps;

    [SerializeField] List<GameObject> gameObjectsToActivate;

    [SerializeField] public RoomTrait[] traits;

    [FoldoutGroup("Events")] public UnityEvent onActivate;

    public bool discovered;
    public string roomName;

    List<LootSpawner> LootSpawners => new(lootSpawnersParent.GetComponentsInChildren<LootSpawner>());

    void OnValidate()
    {
        if (roomName.IsNullOrWhitespace()){
            CopyGameobjectName();
        }
    }

    public void Spawn(Vector3 position, Vector3 dir)
    {
        roomSpawnConfig.Spawn(position, dir);
        foreach (var obj in gameObjectsToActivate) obj.SetActive(false);
    }

    public void Activate()
    {
        SpawnLoot();
        roomTraps?.Activate();
        foreach (var obj in gameObjectsToActivate) obj.SetActive(true);
        onActivate?.Invoke();
    }

    void SpawnLoot()
    {
        foreach (var spawner in LootSpawners) spawner.SpawnGameObjects();
    }

    [Button]
    void CopyGameobjectName()
    {
        roomName = gameObject.name;
    }
}