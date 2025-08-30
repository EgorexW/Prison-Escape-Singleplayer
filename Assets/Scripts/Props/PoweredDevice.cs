using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class PoweredDevice : MonoBehaviour, IPoweredDevice
{
    IPowerSource powerSource;
    public Transform Transform => transform;

    [FoldoutGroup("Events")]
    public UnityEvent onPowerChanged;
    
    protected void Start()
    {
        powerSource = General.GetRootComponent<IPowerSource>(gameObject);
        powerSource.OnPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged();
    }
    
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