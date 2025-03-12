using System;
using UnityEngine;

public class PowerResetButton : MonoBehaviour, IInteractive
{
    public void Interact(Character character)
    {
        var powerSystem = General.GetRootComponent<PowerSystem>(gameObject);
        powerSystem.ResetPower();
    }
}
