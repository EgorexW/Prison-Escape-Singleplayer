using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RoomBackground : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] GameObject defaultRoomToSpawn;
    [BoxGroup("References")][Required][SerializeField] RoomSpawner roomSpawner;
        
    [SerializeField] float doorOpenWait = 0.5f;
    
    IEnumerator Start()
    {
        yield return Activate();
    }

    IEnumerator Activate()
    {
        var room = roomSpawner.SpawnRoom(defaultRoomToSpawn);
        yield return new WaitForSeconds(doorOpenWait);
        room.doorway.GetDoor().Open();
    }
}
