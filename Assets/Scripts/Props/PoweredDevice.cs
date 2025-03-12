using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class PoweredDevice : MonoBehaviour, IPowerSystemDevice
{
    [SerializeField] bool addSelfToPowerSystem = true;
    
    [SerializeField] float empResistance = 1;
    [FoldoutGroup("Events")] [SerializeField] UnityEvent<PowerLevel> onPowerChange;
    public PowerLevel PowerLevel{ get; private set; }
    public float EmpResistance => empResistance;
    public UnityEvent<PowerLevel> OnPowerChange => onPowerChange;
    public IPowerSource PowerSource{ get; set; }
    
    protected void Start()
    {
        if (addSelfToPowerSystem){
            General.GetRootComponent<PowerSystem>(gameObject).AddDevice(this);
        }
    }
    
    public virtual void SetPower(PowerLevel power)
    {
        PowerLevel = power;
        onPowerChange.Invoke(power);
    }

    public void EmpHit(float strenght)
    {
        SetPower(PowerLevel.NoPower);
        General.CallAfterSeconds(ResetPower, strenght/EmpResistance);
    }

    void ResetPower()
    {
        if (PowerSource == null){
            SetPower(PowerLevel.NoPower);
            return;
        }
        SetPower(PowerSource.GetPower(this));
    }
}