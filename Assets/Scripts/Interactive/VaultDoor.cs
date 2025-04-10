using System;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Door))]
public class VaultDoor : MonoBehaviour, IAIObject
{
    [GetComponent][SerializeField] Door door;

    [HideInInspector][SerializeField] List<GameObject> workingLights;

    // float doorBaseOpenTime;
    MainAI mainAI;
    
    void Awake()
    {
        door.onOpen.AddListener(OnDoorOpen);
        // doorBaseOpenTime = door.HoldDuration;
    }

    void OnDoorOpen()
    {
        PlayerMark playerMark = new PlayerMark(transform.position, 0.5f);
        mainAI.PlayerNoticed(playerMark);
    }

    public void Init(MainAI mainAI)
    {
        this.mainAI = mainAI;
    }
}