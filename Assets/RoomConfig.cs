using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomConfig : MonoBehaviour
{
    [FoldoutGroup("References")][Required][SerializeField] TextMeshPro nameText;
    [FoldoutGroup("References")][Required][SerializeField] KeycardReader[] keycardReaders;
    [BoxGroup("References/Doors")] [Required] [SerializeField] GameObject weakDoor;
    [BoxGroup("References/Doors")] [Required] [SerializeField] GameObject strongDoor;

    [FormerlySerializedAs("name")] [SerializeField] string roomName = "Room";
    [SerializeField] AccessLevel accessLevel;
    [SerializeField] DoorType doorType = DoorType.Weak;
    
    void Awake()
    {
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
        keycardReaders = GetComponentsInChildren<KeycardReader>();
    }
}

enum DoorType
{
    Weak,
    Strong
}
