using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RoomTraps : MonoBehaviour
{
    [SerializeField] bool log;
    
    [BoxGroup("References")] [Required] [SerializeField] TrapsConfig trapConfig;
    [FormerlySerializedAs("roomConfig")] [BoxGroup("References")][Required][SerializeField] EntryConfig entryConfig;
    
    [BoxGroup("References")][Required][SerializeField] GameObject poisonTrapPrefab;
    [BoxGroup("References")][Required][SerializeField] Transform poisonTrapSpawnPoint;
    
    ITrap trap;

    void Start()
    {
        entryConfig.onOpen.AddListener(ActivateTrap);
        if (Random.value < trapConfig.trapChance){
            CreateATrap();
        }    
    }

    void CreateATrap()
    {
        CreatePoisonTrap();
    }

    void CreatePoisonTrap()
    {
        var obj = Instantiate(poisonTrapPrefab, poisonTrapSpawnPoint.position, Quaternion.identity, transform);
        obj.transform.localRotation = Quaternion.identity;
        trap = obj.GetComponent<ITrap>();
    }

    void ActivateTrap()
    {
        Log("Activating trap");
        trap?.Activate();
    }

    void Log(string message)
    {
        if (log){
            Debug.Log(message, this);
        }
    }
}


interface ITrap
{
    void Activate();
}