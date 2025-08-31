using UnityEngine;
using UnityEngine.Serialization;

public class PowerButton : MonoBehaviour, IInteractive
{
    [SerializeField] SubPowerSystem targetedSubSystem;
    [SerializeField] PowerLevel targetPowerLevel = PowerLevel.FullPower;


    public void Interact(Player player)
    {
        var powerSystem = General.GetRootComponent<MainPowerSystem>(gameObject);
        if (targetedSubSystem == null){
            powerSystem.ChangePower(transform.position, targetPowerLevel);
        }
        else{
            
        powerSystem.ChangePower(targetedSubSystem, targetPowerLevel);
        }
    }

    public float HoldDuration => 3;
}
