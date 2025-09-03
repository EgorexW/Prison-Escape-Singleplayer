using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class RoomConfig : MonoBehaviour
{
    [FoldoutGroup("References")][Required][SerializeField] TextMeshPro nameText;
    [FoldoutGroup("References")][Required][SerializeField] KeycardReader[] keycardReaders;
    [FoldoutGroup("References")][Required][SerializeField] List<DoorLock> doorLocks; //unused for now
    [BoxGroup("References/Doors")] [Required] [SerializeField] GameObject weakDoor;
    [BoxGroup("References/Doors")] [Required] [SerializeField] GameObject strongDoor;

    [FormerlySerializedAs("name")] [SerializeField] string roomName = "Room";
    [SerializeField] AccessLevel accessLevel;
    [SerializeField] DoorType doorType = DoorType.Weak;
    
    [FoldoutGroup("Events")] public UnityEvent onOpen;

    void Awake()
    {
        foreach (var door in GetComponentsInChildren<Door>(true)){
            door.onOpen.AddListener(onOpen.Invoke);
        }
        ApplyConfig();
    }

    [Button]
    void ApplyConfig()
    {
        nameText.text = roomName;
        if (accessLevel != null){
            foreach (var keycardReader in keycardReaders){
                keycardReader.gameObject.SetActive(true);
                keycardReader.accessLevel = accessLevel;
            }
        }
        weakDoor.SetActive(doorType == DoorType.Weak);
        strongDoor.SetActive(doorType == DoorType.Strong);
    }

    void Reset()
    {
        keycardReaders = GetComponentsInChildren<KeycardReader>(includeInactive: true);
        doorLocks.Clear();
        foreach (var keycardReader in keycardReaders){
            doorLocks.Add(keycardReader.doorLock);
        }
    }
}

enum DoorType
{
    Weak,
    Strong
}
