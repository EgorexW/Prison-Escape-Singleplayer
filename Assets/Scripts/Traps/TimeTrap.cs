using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class TimeTrap : PoweredDevice, ITrap
{
    [BoxGroup("References")][Required][SerializeField] MotionSensor motionSensor;
    
    [SerializeField] float timePenalty = 60f;
    
    [SerializeField] bool isActive;

    void Awake()
    {
        motionSensor.onActivation.AddListener(OnTriggered);
    }

    public void Activate()
    {
        isActive = true;
        OnPowerChanged();
    }

    public void OnTriggered()
    {
        GameDirector.i.gameTime.ChangeTime(-timePenalty);
        isActive = false;
        OnPowerChanged();
    }

    protected override void OnPowerChanged()
    {
        base.OnPowerChanged();
        motionSensor.SetActive(GetPowerLevel() == PowerLevel.FullPower && isActive);
    }
}
