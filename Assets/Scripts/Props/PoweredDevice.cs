using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class PoweredDevice : MonoBehaviour, IPoweredDevice
{
    [FoldoutGroup("Events")] public UnityEvent onPowerChanged;

    IPowerSource powerSource;

    protected void Start()
    {
        powerSource = General.GetRootComponent<IPowerSource>(gameObject);
        powerSource.OnPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged();
    }

    public Transform Transform => transform;

    public PowerLevel GetPowerLevel()
    {
        if (powerSource == null){
            Debug.LogError("No power source found in parent hierarchy");
            return PowerLevel.NoPower;
        }
        return powerSource.GetPower(this);
    }

    protected virtual void OnPowerChanged()
    {
        onPowerChanged.Invoke();
    }
}