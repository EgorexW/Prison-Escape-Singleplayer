using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FishNet.Object;
using UnityEngine;

public class RoomSpawner : NetworkBehaviour
{
    [SerializeField] Transform dir;
    [SerializeField] RoomTrait[] traits;

    public void SpawnRoom(GameObject room){
        room = Instantiate(room, transform.parent);
        SpawnableRoom spawnableRoom = room.GetComponent<SpawnableRoom>();
        spawnableRoom.SetPos(transform.position, dir.position - transform.position);
        Spawn(room);
        Destroy(gameObject);
    }
    public bool HasTrait(Trait trait){
        return traits.Contains(trait);
    }
    public bool HasTraits(Trait[] traits){
        foreach (Trait trait in traits)
        {
            if (!HasTrait(trait)){
                return false;
            }
        }
        return true;
    }
}
