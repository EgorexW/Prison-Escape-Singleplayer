using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomChooser : MonoBehaviour
{
    const int MaxGenerationLoops = 1000;

    [SerializeField] List<Room> necessaryRooms;
    [SerializeField] List<Room> optionalRooms;
    
    public Dictionary<RoomSpawner, Room> ChooseRooms()
    {
        List<RoomSpawner> spawners = new(GetComponentsInChildren<RoomSpawner>());
        List<Room> rooms = new();
        Dictionary<RoomSpawner, Room> matchedRoomsWithSpawners = new();
        rooms.AddRange(necessaryRooms);
        List<Room> optionalRoomsTmp = new List<Room>(optionalRooms);
        while (rooms.Count < spawners.Count){
            Debug.Assert(optionalRoomsTmp.Count > 0, "Not enought rooms", this);
            Room optionalRoom = optionalRoomsTmp[Random.Range(0, optionalRoomsTmp.Count)];
            rooms.Add(optionalRoom);
            optionalRoomsTmp.Remove(optionalRoom);
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
            if (nr > MaxGenerationLoops){
                Debug.LogWarning("Generation looped out", this);
                return null;
            }
        }
        return matchedRoomsWithSpawners;
    }
}
