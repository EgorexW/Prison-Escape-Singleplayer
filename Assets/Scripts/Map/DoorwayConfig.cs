using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class DoorwayConfig : MonoBehaviour
{
    [FoldoutGroup("References")] [Required] [SerializeField] KeycardReader[] keycardReaders;
    [FoldoutGroup("References")] [Required] [SerializeField] DoorLock[] doorLocks;
    [BoxGroup("References/Doors")] [Required] [SerializeField] GameObject weakDoor;
    [BoxGroup("References/Doors")] [Required] [SerializeField] GameObject strongDoor;

    [SerializeField] public AccessLevel accessLevel;
    [SerializeField] DoorType doorType = DoorType.Weak;

    [FoldoutGroup("Events")] public UnityEvent onOpen;

    void Awake()
    {
        foreach (var door in GetComponentsInChildren<Door>(true)) door.onOpen.AddListener(onOpen.Invoke);
        ApplyConfig();
    }

    void Reset()
    {
        keycardReaders = GetComponentsInChildren<KeycardReader>(true);
        doorLocks = GetComponentsInChildren<DoorLock>(true);
    }

    [Button]
    void ApplyConfig()
    {
        foreach (var keycardReader in keycardReaders){
            keycardReader.gameObject.SetActive(accessLevel != null);
            keycardReader.accessLevel = accessLevel;
        }
        foreach (var doorLock in doorLocks) doorLock.unlocked = accessLevel == null;
        weakDoor.SetActive(doorType == DoorType.Weak);
        strongDoor.SetActive(doorType == DoorType.Strong);
    }

    public Door GetDoor()
    {
        return doorType switch{
            DoorType.Weak => weakDoor.GetComponentInChildren<Door>(),
            DoorType.Strong => strongDoor.GetComponentInChildren<Door>(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public List<KeycardReader> GetKeycardReaders()
    {
        return new List<KeycardReader>(keycardReaders);
    }
}

enum DoorType
{
    Weak,
    Strong
}