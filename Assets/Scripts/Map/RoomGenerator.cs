using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private const float MaxGenerationTime = 1f;
    [SerializeField] Optional<int> seed;

    private void Awake()
    {
        if (seed){
            Random.InitState(seed);
        }
        GenerateRooms(GetComponent<RoomChooser>().ChooseRooms());
    }
    void GenerateRooms(Dictionary<RoomSpawner, Room> matchedRoomWithSpawner){
        foreach (KeyValuePair<RoomSpawner, Room> match in matchedRoomWithSpawner)
        {
            match.Key.SpawnRoom(match.Value.GetGameObject());
        }
    }
}