using System.Linq;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] Transform dir;
    [SerializeField] RoomTrait[] traits;

    public void SpawnRoom(GameObject room){
        room = Instantiate(room, transform.parent);
        var spawnableRoom = room.GetComponent<SpawnableRoom>();
        spawnableRoom.SetPos(transform.position, dir.position - transform.position);
        Destroy(gameObject);
    }
    public bool HasTrait(Trait trait){
        return traits.Contains(trait);
    }
    public bool HasTraits(Trait[] traits){
        foreach (var trait in traits)
        {
            if (!HasTrait(trait)){
                return false;
            }
        }
        return true;
    }
}
