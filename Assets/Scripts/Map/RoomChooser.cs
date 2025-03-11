using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomChooser : SerializedMonoBehaviour
{
    const int MAX_GENERATION_LOOPS = 1000;

    [SerializeField] List<Room> necessaryRooms = new List<Room>();
    [SerializeField] Dictionary<Room, int> optionalRooms = new();
    int necessaryRoomsCount => necessaryRooms.Count;
    int optionalRoomsCount => optionalRooms.Sum(x => x.Value);
    [ShowInInspector] int allRooms => necessaryRoomsCount + optionalRoomsCount;
    
    [SerializeField][MinMaxSlider("necessaryRoomsCount", "allRooms")] Vector2Int roomsToSpawn = new(10, 15);

    [SerializeField] Room fillerRoom;

    [ShowInInspector] int roomSpawners => GetComponentsInChildren<RoomSpawner>().Length;
    public Dictionary<RoomSpawner, Room> ChooseRooms()
    {
        List<RoomSpawner> spawners = new(GetComponentsInChildren<RoomSpawner>());
        List<Room> rooms = new();
        Dictionary<RoomSpawner, Room> matchedRoomsWithSpawners = new();
        rooms.AddRange(necessaryRooms);
        int roomsNr = Random.Range(roomsToSpawn.x, roomsToSpawn.y);
        var optionalRoomsLeft = new Dictionary<Room, int>(optionalRooms);
        while (rooms.Count < roomsNr){
            Room optionalRoom = optionalRoomsLeft.WeightedRandom();
            rooms.Add(optionalRoom);
            optionalRoomsLeft[optionalRoom]--;
        }
        while (rooms.Count < spawners.Count){
            rooms.Add(fillerRoom);
        }
        rooms.Shuffle();
        spawners.Shuffle();
        int nr = 0;
        while (rooms.Count > 0)
        {   
            Room room = rooms[0];  
            bool matched = false;     
            foreach (RoomSpawner spawner in spawners.ToArray())
            {
                if (!spawner.HasTraits(room.traits)){
                    continue;
                }
                spawners.Remove(spawner);
                rooms.Remove(room);
                matchedRoomsWithSpawners.Add(spawner, room);
                matched = true;
                break;
            }
            if (matched){
                continue;
            }
            foreach (RoomSpawner spawner in matchedRoomsWithSpawners.Keys.ToArray())
            {
                if (!spawner.HasTraits(room.traits)){
                    continue;
                }
                rooms.Add(matchedRoomsWithSpawners[spawner]);
                matchedRoomsWithSpawners.Remove(spawner);
                rooms.Remove(room);
                matchedRoomsWithSpawners.Add(spawner, room);
                break;
            }
            nr ++;
            if (nr > MAX_GENERATION_LOOPS){
                Debug.LogWarning("Generation looped out", this);
                return null;
            }
        }
        return matchedRoomsWithSpawners;
    }
}
