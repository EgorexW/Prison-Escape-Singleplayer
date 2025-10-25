using UnityEngine;
using UnityEngine.Events;

public interface IPowerSource
{
    UnityEvent OnPowerChanged{ get; }
    PowerLevel GetPower(Vector3 pos);
}

public enum PowerLevel
{
    NoPower = 0,
    MinimalPower = 1,
    FullPower = 2
}