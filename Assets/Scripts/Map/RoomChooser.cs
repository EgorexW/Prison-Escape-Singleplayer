using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class RoomChooser : SerializedMonoBehaviour
{
    const int MAX_GENERATION_LOOPS = 1000;

    [SerializeField] List<GameObject> necessaryRooms = new();
    [SerializeField] Dictionary<GameObject, int> optionalRooms = new();

    [SerializeField] [MinMaxSlider("necessaryRoomsCount", "@Mathf.Min(allRooms, roomSpawners)")]
    Vector2Int roomsToSpawn = new(10, 15);

    [SerializeField] GameObject fillerRoom;
    int necessaryRoomsCount => necessaryRooms.Count;
    int optionalRoomsCount => optionalRooms.Count != 0 ? optionalRooms.Sum(x => x.Value) : 0;
    [ShowInInspector] int allRooms => necessaryRoomsCount + optionalRoomsCount;

    [ShowInInspector] int roomSpawners => GetComponentsInChildren<RoomSpawner>().Length;

    public Dictionary<RoomSpawner, GameObject> ChooseRooms()
    {
        List<RoomSpawner> spawners = new(GetComponentsInChildren<RoomSpawner>());
        List<GameObject> rooms = new();
        Dictionary<RoomSpawner, GameObject> matchedRoomsWithSpawners = new();
        rooms.AddRange(necessaryRooms);
        var roomsNr = Random.Range(roomsToSpawn.x, roomsToSpawn.y);
        var optionalRoomsLeft = new Dictionary<GameObject, int>(optionalRooms);
        while (rooms.Count < roomsNr){
            var optionalRoom = optionalRoomsLeft.WeightedRandom();
            rooms.Add(optionalRoom);
            optionalRoomsLeft[optionalRoom]--;
        }
        while (rooms.Count < spawners.Count) rooms.Add(fillerRoom);
        rooms.Shuffle();
        spawners.Shuffle();
        var nr = 0;
        while (rooms.Count > 0){
            var room = rooms[0];
            var matched = false;
            foreach (var spawner in spawners.ToArray()){
                if (!spawner.HasTraits(room.GetComponent<SpawnableRoom>().traits)){
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
            foreach (var spawner in matchedRoomsWithSpawners.Keys.ToArray()){
                if (!spawner.HasTraits(room.GetComponent<SpawnableRoom>().traits)){
                    continue;
                }
                rooms.Add(matchedRoomsWithSpawners[spawner]);
                matchedRoomsWithSpawners.Remove(spawner);
                rooms.Remove(room);
                matchedRoomsWithSpawners.Add(spawner, room);
                break;
            }
            nr++;
            if (nr > MAX_GENERATION_LOOPS){
                Debug.LogWarning("Generation looped out", this);
                return null;
            }
        }
        return matchedRoomsWithSpawners;
    }
}