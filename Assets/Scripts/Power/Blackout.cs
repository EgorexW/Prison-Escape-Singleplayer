using UnityEngine;

public class Blackout : MonoBehaviour, ITrap
{
    public void Activate()
    {
        var powerSystem = MainPowerSystem.i;
        powerSystem.SetGlobalMinimalPower(false);
        powerSystem.ChangePower(transform.position, PowerLevel.NoPower);
    }
}