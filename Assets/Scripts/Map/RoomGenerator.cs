using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomChooser))]
public class RoomGenerator : MonoBehaviour
{
    const int GenerationTries = 10;
    [SerializeField] Optional<int> seed;

    public void Generate()
    {
        if (seed){
            Random.InitState(seed);
        }
        var roomChooser = GetComponent<RoomChooser>();
        for (var i = 0; i < GenerationTries; i++){
            var choosenRooms = roomChooser.ChooseRooms();
            if (choosenRooms != null){
                GenerateRooms(choosenRooms);
                return;
            }
        }
        Debug.LogError("Generation Failed", this);
    }

    void GenerateRooms(Dictionary<RoomSpawner, GameObject> matchedRoomWithSpawner)
    {
        foreach (var match in matchedRoomWithSpawner) match.Key.SpawnRoom(match.Value);
    }
}