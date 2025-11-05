using UnityEngine;

public class Blackout : MonoBehaviour, ITrap
{
    [SerializeField] PowerLevel targetPowerLevel = PowerLevel.NoPower;
    [SerializeField] bool removeGlobalMinimalPower = true;

    public void Activate()
    {
        var powerSystem = MainPowerSystem.i;
        if (removeGlobalMinimalPower){
            powerSystem.SetGlobalMinimalPower(false);
        }
        if (powerSystem.GetPower(transform.position) <= targetPowerLevel){
            return;
        }
        powerSystem.ChangePower(transform.position, targetPowerLevel);
    }
}