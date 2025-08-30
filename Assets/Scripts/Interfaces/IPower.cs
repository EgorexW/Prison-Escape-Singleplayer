using UnityEngine;
using UnityEngine.Events;

public interface IElectric
{
    float EmpResistance{ get; }
    
    void EmpHit(float strenght);
}

public interface IPoweredDevice
{
    Transform Transform{ get; }
}

public interface IPowerSource
{
    PowerLevel GetPower(IPoweredDevice poweredDevice);
    UnityEvent OnPowerChanged{ get; }
}

public enum PowerLevel
{
    FullPower,
    MinimalPower,
    NoPower
}