using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MainPowerSystem : SerializedMonoBehaviour, IPowerSource
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
}