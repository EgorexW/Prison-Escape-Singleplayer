using System.Collections.Generic;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    [SerializeField] float resetTime = 10f;
    
    HashSet<IPowerSystemDevice> devices = new();
    
    PowerState state = PowerState.Normal;

    public void AddDevice(IPowerSystemDevice device)
    {
        devices.Add(device);
    }

    public void ResetPower()
    {
        foreach (var device in devices)
        {
            device.SetPower(false);            
        }
        state = PowerState.Reseting;
        General.CallAfterSeconds(PowerAllOnAfterReset, resetTime);
    }

    void PowerAllOnAfterReset()
    {
        if (state != PowerState.Reseting){
            return;
        }
        foreach (var device in devices)
        {
            device.SetPower(true);
        }
        state = PowerState.Normal;
    }
}

enum PowerState
{
    Normal,
    Reseting
}

public interface IElectric
{
    void EmpHit(float strenght);
}

public interface IPowerSystemDevice : IElectric
{
    void SetPower(bool power);
}
