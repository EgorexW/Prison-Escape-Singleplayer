using UnityEngine.Events;

public interface IElectric
{
    float EmpResistance{ get; }
    
    void EmpHit(float strenght);
}

public interface IPowerSystemDevice : IElectric
{
    IPowerSource PowerSource{ get; set; }
    PowerLevel PowerLevel{ get; }
    
    UnityEvent<PowerLevel> OnPowerChange{ get; }
    
    void SetPower(PowerLevel power);
}

public interface IPowerSource
{
    PowerLevel GetPower(IPowerSystemDevice powerSystemDevice);
}

public enum PowerLevel
{
    FullPower,
    MinimalPower,
    NoPower
}