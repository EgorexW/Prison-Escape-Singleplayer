using System;
using UnityEngine;

public class PowerResetButton : MonoBehaviour, IInteractive
{
    [SerializeField] float holdDuration = 1;

    public float HoldDuration => holdDuration;
    
    public void Interact(Character character)
    {
        var powerSystem = General.GetRootComponent<PowerSystem>(gameObject);
        powerSystem.ResetPower();
    }
}
