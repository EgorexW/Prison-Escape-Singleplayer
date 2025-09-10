using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class PoweredDevice : MonoBehaviour, IPoweredDevice
{
    [FoldoutGroup("Events")] public UnityEvent<PowerLevel> onPowerChanged;

    IPowerSource powerSource;

    protected virtual void Start()
    {
        if (powerSource == null){
            SetPowerSource(MainPowerSystem.i);
        }
    }

    public void SetPowerSource(IPowerSource powerSource)
    {
        this.powerSource = powerSource;
        powerSource.OnPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged();
    }

    public Transform Transform => transform;

    public PowerLevel GetPowerLevel()
    {
        if (powerSource == null){
            SetPowerSource(MainPowerSystem.i);
        }
        return powerSource.GetPower(this);
    }

    protected virtual void OnPowerChanged()
    {
        onPowerChanged.Invoke(GetPowerLevel());
    }
    
    void OnDestroy()
    {
        if (powerSource != null){
            powerSource.OnPowerChanged.RemoveListener(OnPowerChanged);
        }
    }
}