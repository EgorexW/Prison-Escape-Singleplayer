using System;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Door))]
public class VaultDoor : PoweredDevice
{
    [GetComponent][SerializeField] Door door;

    [SerializeField] float minimalPowerOpenTime = 2;
    [SerializeField] float noPowerOpenTime = 10;

    [SerializeField] List<GameObject> workingLights;

    float doorBaseOpenTime;

    void Awake()
    {
        doorBaseOpenTime = door.HoldDuration;
    }

    public override void SetPower(PowerLevel power)
    {
        base.SetPower(power);
        door.holdDuration = power switch{
            PowerLevel.FullPower => doorBaseOpenTime,
            PowerLevel.MinimalPower => doorBaseOpenTime * minimalPowerOpenTime,
            PowerLevel.NoPower => doorBaseOpenTime * noPowerOpenTime,
            _ => throw new ArgumentOutOfRangeException(nameof(power), power, null)
        };
        foreach (var lightTmp in workingLights){
            lightTmp.SetActive(power != PowerLevel.NoPower);
        }
    }
}