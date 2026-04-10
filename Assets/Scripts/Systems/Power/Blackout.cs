using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Blackout : MonoBehaviour, ITrap //TODO Seperate EMP blackout, from blackout trap
{
    [SerializeField] PowerLevel targetPowerLevel = PowerLevel.NoPower;
    [SerializeField] bool removeGlobalMinimalPower = true;
    
    [FoldoutGroup("Events")] public UnityEvent onActivate;

    [FoldoutGroup("Events")]
    public UnityEvent onBlackout;
    
    bool trapActive;

    public void Activate()
    {
        onActivate.Invoke();
        trapActive = true;
    }

    public void ActivateBlackout()
    {
        var powerSystem = MainPowerSystem.i;
        if (removeGlobalMinimalPower){
            powerSystem.SetGlobalMinimalPower(false);
        }
        onBlackout.Invoke();
        if (powerSystem.GetPower(transform.position) <= targetPowerLevel){
            return;
        }
        powerSystem.ChangePower(transform.position, targetPowerLevel);
    }

    public bool Eligable(Room room)
    {
        return true;
    }

    public void SetRoom(Room room)
    {
        room.roomNode.onPlayerEnteredRoom.AddListener(ActivateBlackout);
    }

    void ActivateBlackout(Room arg0)
    {
        if (!trapActive){
            return;
        }
        trapActive = false;
        ActivateBlackout();
    }
}