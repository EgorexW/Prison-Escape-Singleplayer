using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(RoomChooser))]
public class RoomGenerator : MonoBehaviour
{
    const int GenerationTries = 10;
    [SerializeField] [HideIf("setSeedBasedOnGameNr")] Optional<int> seed;
    [SerializeField] bool setSeedBasedOnGameNr = true;

    public List<Room> GenerateRooms()
    {
        if (setSeedBasedOnGameNr){
            var seedValue = PlayerPrefs.GetInt("Games Started", 0);
            Random.InitState(seedValue);
        }
        else if (seed){
            Random.InitState(seed);
        }
        var roomChooser = GetComponent<RoomChooser>();
        for (var i = 0; i < GenerationTries; i++){
            var choosenRooms = roomChooser.ChooseRooms();
            if (choosenRooms != null){
                return GenerateRooms(choosenRooms);
            }
            else{
                Debug.LogWarning("Room generation failed, retrying... (" + (i + 1) + "/" + GenerationTries + ")", this);
            }
        }
        throw new Exception("Failed to generate rooms after " + GenerationTries + " tries");
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