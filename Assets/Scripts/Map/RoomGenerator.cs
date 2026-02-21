using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomChooser))]
public class RoomGenerator : MonoBehaviour
{
    public List<Room> GenerateRooms()
    {
        var roomChooser = GetComponent<RoomChooser>();
        for (var i = 0; i < General.Iterationlimit; i++){
            var choosenRooms = roomChooser.ChooseRooms();
            if (choosenRooms != null){
                return GenerateRooms(choosenRooms);
            }
            Debug.LogWarning("Room generation failed, retrying... (" + (i + 1) + "/" + General.Iterationlimit + ")", this);
        }
        throw new Exception("Failed to generate rooms after " + General.Iterationlimit + " tries");
    }

    List<Room> GenerateRooms(Dictionary<RoomSpawner, GameObject> matchedRoomWithSpawner)
    {
        var spawnedRooms = new List<Room>();
        foreach (var match in matchedRoomWithSpawner) spawnedRooms.Add(match.Key.SpawnRoom(match.Value));
        return spawnedRooms;
    }
}