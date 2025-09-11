using UnityEngine;
using UnityEngine.Events;

public interface IPoweredDevice
{
    Transform Transform{ get; }
}

public interface IPowerSource
{
    UnityEvent OnPowerChanged{ get; }
    PowerLevel GetPower(IPoweredDevice poweredDevice);
}

public enum PowerLevel
{
    NoPower = 0,
    MinimalPower = 1,
    FullPower = 2
}