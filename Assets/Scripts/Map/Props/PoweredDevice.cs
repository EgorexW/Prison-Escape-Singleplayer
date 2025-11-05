using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PoweredDevice : MonoBehaviour
{
    [SerializeField] PowerLevel workingPower = PowerLevel.FullPower;

    [FoldoutGroup("Events")] public UnityEvent<bool> onPowerChanged;

    IPowerSource powerSource;

    public Transform Transform => transform;

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
        return powerSource.GetPower(transform.position);
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