using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PoweredDevice : MonoBehaviour, IPoweredDevice
{
    [SerializeField] PowerLevel workingPower = PowerLevel.FullPower;
    
    [FoldoutGroup("Events")] public UnityEvent<bool> onPowerChanged;

    IPowerSource powerSource;

    protected virtual void Start()
    {
        if (powerSource == null){
            SetPowerSource(MainPowerSystem.i);
        }
    }

    void OnDestroy()
    {
        if (powerSource != null){
            powerSource.OnPowerChanged.RemoveListener(OnPowerChanged);
        }
    }

    public Transform Transform => transform;

    public void SetPowerSource(IPowerSource powerSource)
    {
        this.powerSource = powerSource;
        powerSource.OnPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged();
    }

    protected PowerLevel GetPowerLevel()
    {
        if (powerSource == null){
            SetPowerSource(MainPowerSystem.i);
        }
        return powerSource.GetPower(this);
    }
    
    public bool IsPowered()
    {
        var powerLevel = GetPowerLevel();
        return powerLevel >= workingPower;
    }

    protected virtual void OnPowerChanged()
    {
        onPowerChanged.Invoke(IsPowered());
    }
}