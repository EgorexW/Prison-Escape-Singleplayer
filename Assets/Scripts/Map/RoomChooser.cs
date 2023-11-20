using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomChooser : MonoBehaviour
{
    [SerializeField] List<Room> necessaryRooms;
    [SerializeField] List<Room> optionalRooms;
    
    public Dictionary<RoomSpawner, Room> ChooseRooms()
    {
        List<RoomSpawner> spawners = new(GetComponentsInChildren<RoomSpawner>());
        List<Room> rooms = new();
        Dictionary<RoomSpawner, Room> matchedRoomsWithSpawners = new();
        rooms.AddRange(necessaryRooms);
        while (rooms.Count < spawners.Count){
            Debug.Assert(optionalRooms.Count > 0, "Not enought rooms", this);
            Room optionalRoom = optionalRooms[Random.Range(0, optionalRooms.Count)];
            rooms.Add(optionalRoom);
            optionalRooms.Remove(optionalRoom);
        }
        rooms.Shuffle();
        spawners.Shuffle();
        // float startTime = Time.unscaledTime;
        while (rooms.Count > 0)
        {   
            // Debug.Log(Time.unscaledTime);
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
            // Debug.Assert(Time.unscaledTime - startTime > MaxGenerationTime, "Generation timed out");
        }
        return matchedRoomsWithSpawners;
    }
}
