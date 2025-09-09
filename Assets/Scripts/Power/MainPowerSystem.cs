using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MainPowerSystem : MonoBehaviour, IPowerSource
{
    [SerializeField] List<PowerLevel> startPowerDistribution;
    [SerializeField] List<SubPowerSystem> subPowerSystems;
    [SerializeField] float timeBetweenPowerLoss = 90f;
    float lastPowerLossTime;

    public List<SubPowerSystem> SubPowerSystems => subPowerSystems.Copy();
    public bool GlobalMinimalPower{ get; private set; }

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