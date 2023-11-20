using System.Collections.Generic;
using System.Linq;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class RoomGenerator : NetworkBehaviour
{
    private const float MaxGenerationTime = 1f;
    [SerializeField] Optional<int> seed;

    public override void OnStartServer()
    {
        base.OnStartServer();
        if (base.IsServer)
        {
            if (!seed){
                Random.InitState(seed);
            }
            GenerateRooms(GetComponent<RoomChooser>().ChooseRooms());
        }
    }
    void GenerateRooms(Dictionary<RoomSpawner, Room> matchedRoomWithSpawner){
        foreach (KeyValuePair<RoomSpawner, Room> match in matchedRoomWithSpawner)
        {
            match.Key.SpawnRoom(match.Value.GetGameObject());
        }
    }
}