using UnityEngine;

public class Blackout : MonoBehaviour, ITrap
{
    [SerializeField] bool removeGlobalMinimalPower = true;

    public void Activate()
    {
        var powerSystem = MainPowerSystem.i;
        if (removeGlobalMinimalPower){
            powerSystem.SetGlobalMinimalPower(false);
        }
        powerSystem.ChangePower(transform.position, PowerLevel.NoPower);
    }
}