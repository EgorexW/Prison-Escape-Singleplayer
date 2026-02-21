using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RoomTraps : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Room room;

    [BoxGroup("References")] [Required] [SerializeField] TrapsConfig trapConfig;

    [FormerlySerializedAs("entryConfig")]
    [FormerlySerializedAs("roomConfig")]
    [BoxGroup("References")]
    [Required]
    [SerializeField]
    DoorwayConfig doorwayConfig;

    ITrap trap;

    public void Activate()
    {
        doorwayConfig.onOpen.AddListener(ActivateTrap);
        if (Random.value < trapConfig.trapNrMod * GameManager.i.trapsManager.trapChance){
            CreateATrap();
        }
    }

    void CreateATrap()
    {
        GameObject prefab = null;
        for (int i = 0; i < General.Iterationlimit; i++){
            prefab = trapConfig.GetTrapPrefab();
            var trapTmp = prefab.GetComponent<ITrap>();
            if (trapTmp.Eligable(room)){
                break;
            }
        }
        var obj = Instantiate(prefab, transform);
        trap = obj.GetComponent<ITrap>();
        trap.SetRoom(room);
    }

    void ActivateTrap()
    {
        trap?.Activate();
    }
}


interface ITrap
{
    void Activate() { }

    void SetRoom(Room room) { }
    bool Eligable(Room room);
}