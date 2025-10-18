using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(RoomChooser))]
public class RoomGenerator : MonoBehaviour
{
    const int GENERATION_TRIES = 10;

    public List<Room> GenerateRooms()
    {
        var roomChooser = GetComponent<RoomChooser>();
        for (var i = 0; i < GENERATION_TRIES; i++){
            var choosenRooms = roomChooser.ChooseRooms();
            if (choosenRooms != null){
                return GenerateRooms(choosenRooms);
            }
            else{
                Debug.LogWarning("Room generation failed, retrying... (" + (i + 1) + "/" + GENERATION_TRIES + ")", this);
            }
        }
        throw new Exception("Failed to generate rooms after " + GENERATION_TRIES + " tries");
    }

    List<Room> GenerateRooms(Dictionary<RoomSpawner, GameObject> matchedRoomWithSpawner)
    {
        var spawnedRooms = new List<Room>();
        foreach (var match in matchedRoomWithSpawner){
            spawnedRooms.Add(match.Key.SpawnRoom(match.Value));
        }
        return spawnedRooms;
    }
}