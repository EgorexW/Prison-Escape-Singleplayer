using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] Transform dir;
    [SerializeField] RoomTrait[] traits;

    public Room SpawnRoom(GameObject room)
    {
        room = Instantiate(room, transform.parent);
        var spawnableRoom = room.GetComponent<Room>();
        spawnableRoom.Spawn(transform.position, dir.position - transform.position);
        Destroy(gameObject);
        return spawnableRoom;
    }

    public bool HasTrait(RoomTrait trait)
    {
        return traits.Contains(trait);
    }

    public bool HasTraits(RoomTrait[] traits)
    {
        foreach (var trait in traits)
            if (!HasTrait(trait)){
                return false;
            }
        return true;
    }

    [Button]
    [BoxGroup("Directions")]
    public void FaceNorth()
    {
        FaceDir(Vector3.forward);
    }

    [Button]
    [BoxGroup("Directions")]
    public void FaceEast()
    {
        FaceDir(Vector3.right);
    }

    [Button]
    [BoxGroup("Directions")]
    public void FaceSouth()
    {
        FaceDir(Vector3.back);
    }

    [Button]
    [BoxGroup("Directions")]
    public void FaceWest()
    {
        FaceDir(Vector3.left);
    }

    void FaceDir(Vector3 targetDir)
    {
        dir.localPosition = targetDir.normalized * 3;
    }
}