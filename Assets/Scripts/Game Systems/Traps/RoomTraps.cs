using System.Collections.Generic;
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

    List<ITrap> traps = new List<ITrap>();
    
    List<GameObject> trapPrefabsUsed = new List<GameObject>();

    public void Activate()
    {
        doorwayConfig.onOpen.AddListener(ActivateTrap);
        var chance = trapConfig.trapNrMod * GameManager.i.trapsManager.trapChance;
        chance = Mathf.Min(chance, GameManager.i.trapsManager.maxTrapAmount);
        while (Random.value < chance){
            CreateATrap();
            chance--;
        }
    }

    void CreateATrap()
    {
        GameObject prefab = null;
        for (int i = 0; i < General.Iterationlimit; i++){
            prefab = trapConfig.GetTrapPrefab();
            if (trapPrefabsUsed.Contains(prefab)){
                continue;
            }
            var trapTmp = prefab.GetComponent<ITrap>();
            if (trapTmp.Eligable(room)){
                break;
            }
        }
        trapPrefabsUsed.Add(prefab);
        var obj = Instantiate(prefab, transform);
        var trap = obj.GetComponent<ITrap>();
        trap.SetRoom(room);
        traps.Add(trap);
    }

    void ActivateTrap()
    {
        foreach (var trap in traps){
            trap.Activate();
        }
    }
}


interface ITrap
{
    void Activate() { }

    void SetRoom(Room room) { }
    bool Eligable(Room room);
}