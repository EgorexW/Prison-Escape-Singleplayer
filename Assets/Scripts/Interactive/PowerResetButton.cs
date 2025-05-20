using UnityEngine;

public class PowerResetButton : MonoBehaviour, IInteractive
{
    [SerializeField] float holdDuration = 1;

    public float HoldDuration => holdDuration;
    
    public void Interact(Player player)
    {
        var powerSystem = General.GetRootComponent<PowerSystem>(gameObject);
        powerSystem.ResetPower();
    }
}
