using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MainPowerSystem : MonoBehaviour, IPowerSource
{
    [SerializeField] List<SubPowerSystem> subPowerSystems;
    
    public PowerLevel GetPower(IPoweredDevice poweredDevice)
    {
        foreach (var subSystem in subPowerSystems){
            if (subSystem.InBounds(poweredDevice.Transform.position)){
                return subSystem.power;
            }
        }
        Debug.LogError("Device not in any subsystem bounds", poweredDevice.Transform);
        return PowerLevel.NoPower;
    }

    public UnityEvent OnPowerChanged{ get; } = new UnityEvent();

    public void ChangePower(SubPowerSystem targetedSubSystem, PowerLevel fullPower)
    {
        targetedSubSystem.power = fullPower;
        OnPowerChanged.Invoke();
    }

    public void ChangePower(Vector3 targetedSubSystem, PowerLevel targetPowerLevel)
    {
        var subSystem = subPowerSystems.FirstOrDefault(subSystem => subSystem.InBounds(targetedSubSystem));
        if (subSystem == null){
            
        Debug.LogError("No subsystem found at position " + targetedSubSystem);
        }
        ChangePower(subSystem, targetPowerLevel);
    }
}