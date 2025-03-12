using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PowerSystem : SerializedMonoBehaviour, IPowerSource
{
    [SerializeField] float resetTime = 30f;

    [SerializeField] Dictionary<PowerLevel, float> powerLevelDistribiution = new()
    {
        { PowerLevel.FullPower, 6f },
        { PowerLevel.MinimalPower, 3f },
        { PowerLevel.NoPower, 1f }
    };

    [SerializeField] Dictionary<PowerLevel, float> powerLevelDistribiutionIncrement = new()
    {
        { PowerLevel.FullPower, 0 },
        { PowerLevel.MinimalPower, 1 },
        { PowerLevel.NoPower, 2 }
    };

    [SerializeField] Dictionary<PowerLevel, float> powerLevelDistribiutionAfterReset = new()
    {
        { PowerLevel.FullPower, 9 },
        { PowerLevel.MinimalPower, 1 },
        { PowerLevel.NoPower, 0.0f }
    };

    [SerializeField] Vector2Int timeBetweenPowerChanges = new Vector2Int(30, 60); 
    
    HashSet<IPowerSystemDevice> devices = new();
    
    PowerState state = PowerState.Normal;
    float nextPowerChange;

    void Awake()
    {
        GenerateNextPowerChangeTime();
    }

    void Update()
    {
        if (Time.time > nextPowerChange){
            PowerChanges();
        }
    }

    void PowerChanges()
    {
        foreach (var pair in powerLevelDistribiutionIncrement){
            powerLevelDistribiution[pair.Key] += pair.Value;
        }
        ReapplyPower();
        GenerateNextPowerChangeTime();
    }

    public void AddDevice(IPowerSystemDevice device)
    {
        devices.Add(device);
        device.PowerSource = this;
        device.SetPower(GetPower(device));
    }

    public void ResetPower()
    {
        foreach (var device in devices)
        {
            device.SetPower(PowerLevel.NoPower);            
        }
        nextPowerChange = Mathf.Infinity;
        state = PowerState.Reseting;
        General.CallAfterSeconds(PowerAllOnAfterReset, resetTime);
    }

    void PowerAllOnAfterReset()
    {
        if (state != PowerState.Reseting){
            return;
        }
        powerLevelDistribiution = new(powerLevelDistribiutionAfterReset);
        ReapplyPower();
        GenerateNextPowerChangeTime();
        state = PowerState.Normal;
    }

    void ReapplyPower()
    {
        foreach (var device in devices)
        {
            device.SetPower(GetPower(device));
        }
    }

    void GenerateNextPowerChangeTime()
    {
        nextPowerChange =  Time.time + General.RandomRange(timeBetweenPowerChanges);
    }

    public PowerLevel GetPower(IPowerSystemDevice powerSystemDevice)
    {
        var power = powerLevelDistribiution.WeightedRandom();
        return power;
    }
}

enum PowerState
{
    Normal,
    Reseting
}
