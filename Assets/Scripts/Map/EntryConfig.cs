using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EntryConfig : MonoBehaviour
{
    [FoldoutGroup("References")] [Required] [SerializeField] TextMeshPro nameText;
    [FoldoutGroup("References")] [Required] [SerializeField] KeycardReader[] keycardReaders;
    [FoldoutGroup("References")] [Required] [SerializeField] List<DoorLock> doorLocks; //unused for now
    [BoxGroup("References/Doors")] [Required] [SerializeField] GameObject weakDoor;
    [BoxGroup("References/Doors")] [Required] [SerializeField] GameObject strongDoor;

    [FormerlySerializedAs("name")] [SerializeField] string roomName = "Room";
    [SerializeField] AccessLevel accessLevel;
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
        doorLocks.Clear();
        foreach (var keycardReader in keycardReaders) doorLocks.Add(keycardReader.doorLock);
    }

    [Button]
    void ApplyConfig()
    {
        nameText.text = roomName;
        foreach (var keycardReader in keycardReaders){
            keycardReader.gameObject.SetActive(accessLevel != null);
            keycardReader.accessLevel = accessLevel;
        }
        foreach (var doorLock in doorLocks) doorLock.unlocked = accessLevel == null;
        weakDoor.SetActive(doorType == DoorType.Weak);
        strongDoor.SetActive(doorType == DoorType.Strong);
    }
}

enum DoorType
{
    Weak,
    Strong
}