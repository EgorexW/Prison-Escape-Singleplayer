using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class RoomBackground : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PrefabList roomsList;

    [BoxGroup("References")] [Required] [SerializeField] GameObject defaultRoomToSpawn;
    [BoxGroup("References")] [Required] [SerializeField] RoomSpawner roomSpawner;

    [SerializeField] float doorOpenWait = 0.5f;

    IEnumerator Start()
    {
        yield return Activate();
    }

    IEnumerator Activate()
    {
        var lastRoomIndex = PlayerPrefs.GetInt("Last Room Entered", -1);
        var roomToSpawn = defaultRoomToSpawn;
        if (lastRoomIndex >= 0){
            roomToSpawn = roomsList.prefabs[lastRoomIndex];
        }
        var room = roomSpawner.SpawnRoom(roomToSpawn);
        Debug.Log($"Background room spawned: {room.roomName}");
        yield return new WaitForSeconds(doorOpenWait);
        room.doorway?.GetDoor().Open();
    }
}