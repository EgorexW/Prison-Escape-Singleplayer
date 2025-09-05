using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : PoweredDevice
{
    [BoxGroup("References")] [Required] [SerializeField] GameObject laser;
    
    public UnityEvent onActivation;

    void OnTriggerEnter(Collider other)
    {
        onActivation.Invoke();
    }
    
    protected override void OnPowerChanged()
    {
        base.OnPowerChanged();
        laser.SetActive(GetPowerLevel() != PowerLevel.NoPower);
    }
}
