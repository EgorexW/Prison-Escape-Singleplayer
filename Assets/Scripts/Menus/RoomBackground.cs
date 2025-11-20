using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class RoomBackground : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PrefabList roomsList;

    [BoxGroup("References")] [Required] [SerializeField] GameObject defaultRoomToSpawn;
    [BoxGroup("References")] [Required] [SerializeField] RoomSpawner roomSpawner;
    [BoxGroup("References")][Required][SerializeField] CameraMenuMove cameraMenuMove;

    [SerializeField] float doorOpenWait = 0.5f;

    Room spawnedRoom;
    void Start()
    {
        Activate();
    }

    void Activate()
    {
        var lastRoomIndex = PlayerPrefs.GetInt("Last Room Entered", -1);
        var roomToSpawn = defaultRoomToSpawn;
        if (lastRoomIndex >= 0){
            roomToSpawn = roomsList.prefabs[lastRoomIndex];
        }
        SpawnRoom(roomToSpawn);
    }

    public void SpawnRoom(GameObject roomToSpawn)
    {
        StartCoroutine(SpawnRoomCoroutine(roomToSpawn));
    }

    IEnumerator SpawnRoomCoroutine(GameObject roomToSpawn)
    {
        if (spawnedRoom != null){
            Destroy(spawnedRoom.gameObject);
        }
        spawnedRoom = roomSpawner.SpawnRoom(roomToSpawn);
        cameraMenuMove.StartSequence();
        Debug.Log($"Background room spawned: {spawnedRoom.roomName}");
        yield return new WaitForSeconds(doorOpenWait);
        spawnedRoom.doorway?.GetDoor().Open();
    }
}