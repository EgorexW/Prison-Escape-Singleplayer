using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RoomTraps : MonoBehaviour
{
    [SerializeField] bool log;

    [BoxGroup("References")] [Required] [SerializeField] TrapsConfig trapConfig;
    [FormerlySerializedAs("entryConfig")] [FormerlySerializedAs("roomConfig")] [BoxGroup("References")] [Required] [SerializeField] DoorwayConfig doorwayConfig;

    ITrap trap;

    void Start()
    {
        doorwayConfig.onOpen.AddListener(ActivateTrap);
        if (Random.value < trapConfig.trapChance){
            CreateATrap();
        }
    }

    void CreateATrap()
    {
        var prefab = trapConfig.GetTrapPrefab();
        var obj = Instantiate(prefab, transform);
        // obj.transform.localRotation = Quaternion.identity;
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