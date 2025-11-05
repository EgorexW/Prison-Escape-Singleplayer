using UnityEngine;
using UnityEngine.Events;

public interface IPowerSource
{
    UnityEvent OnPowerChanged{ get; }
    PowerLevel GetPower(Vector3 pos);
}

public enum PowerLevel
{
    NoPower,
    MinimalPower,
    FullPower
}