using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MainPowerSystem : MonoBehaviour, IPowerSource
{
    public static MainPowerSystem i { get; private set; }
    
    [SerializeField] List<PowerLevel> startPowerDistribution;
    [SerializeField] List<SubPowerSystem> subPowerSystems;
    [SerializeField] float timeBetweenPowerLoss = 90f;
    
    public List<SubPowerSystem> SubPowerSystems => subPowerSystems.Copy();

    [ShowInInspector][BoxGroup("Debug")] float lastPowerLossTime;
    [ShowInInspector][BoxGroup("Debug")] public bool GlobalMinimalPower{ get; private set; }

    void Awake()
    {
        if (i != null && i != this){
            Debug.LogWarning("There was another instance", this);
            Destroy(gameObject);
            return;
        }
        i = this;
    }

    void Start()
    {
        lastPowerLossTime = Time.time;
        startPowerDistribution.Shuffle();
        for (var i = 0; i < subPowerSystems.Count; i++)
            ChangePower(subPowerSystems[i], startPowerDistribution[i % startPowerDistribution.Count]);
    }

    void Update()
    {
        if (Time.time - lastPowerLossTime > timeBetweenPowerLoss){
            LosePower();
        }
    }

    public UnityEvent OnPowerChanged{ get; } = new();

    public PowerLevel GetPower(IPoweredDevice poweredDevice)
    {
        foreach (var subSystem in subPowerSystems)
            if (subSystem.InBounds(poweredDevice.Transform.position)){
                var subSystemPower = subSystem.power;
                if (GlobalMinimalPower && subSystemPower == PowerLevel.NoPower){
                    subSystemPower = PowerLevel.MinimalPower;
                }
                return subSystemPower;
            }
        Debug.LogError("Device not in any subsystem bounds", poweredDevice.Transform);
        return PowerLevel.NoPower;
    }


    public void ChangePower(SubPowerSystem targetedSubSystem, PowerLevel targetPower)
    {
        if (targetPower != PowerLevel.NoPower){
            lastPowerLossTime = Time.time;
        }
        targetedSubSystem.power = targetPower;
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

    public void SetGlobalMinimalPower(bool minimalPower)
    {
        GlobalMinimalPower = minimalPower;
        OnPowerChanged.Invoke();
    }

    void LosePower()
    {
        var targetedSubSystem = SubPowerSystems.Random();
        ChangePower(targetedSubSystem, PowerLevel.NoPower);
        lastPowerLossTime = Time.time;
    }
}