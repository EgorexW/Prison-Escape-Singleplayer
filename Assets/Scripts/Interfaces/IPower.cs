using UnityEngine;
using UnityEngine.Events;

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