using UnityEngine;

public class Blackout : MonoBehaviour
{
    public void Activate()
    {
        var powerSystem = General.GetRootComponent<MainPowerSystem>(gameObject);
        powerSystem.SetGlobalMinimalPower(false);
        powerSystem.ChangePower(transform.position, PowerLevel.NoPower);
    }
}
