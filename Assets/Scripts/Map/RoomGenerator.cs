using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private const int GenerationTries = 10;
    [SerializeField] Optional<int> seed;

    private void Awake()
    {
        if (seed){
            Random.InitState(seed);
        }
        RoomChooser roomChooser = GetComponent<RoomChooser>();
        for (int i = 0; i < GenerationTries; i++)
        {
            Dictionary<RoomSpawner, Room> choosenRooms = roomChooser.ChooseRooms();
            if (choosenRooms != null){
                GenerateRooms(choosenRooms);
                return;
            }
        } 
        Debug.LogError("Generation Failed", this);
    }
    void GenerateRooms(Dictionary<RoomSpawner, Room> matchedRoomWithSpawner){
        foreach (KeyValuePair<RoomSpawner, Room> match in matchedRoomWithSpawner)
        {
            match.Key.SpawnRoom(match.Value.GetGameObject());
        }
    }
}