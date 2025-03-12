using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PoweredLight : PoweredDevice, IDamagable
{
    [Required][SerializeField] MeshRenderer meshRenderer;
    [GetComponent] [SerializeField] Light light;

    [Required][SerializeField] Material defaultMaterial;
    [FormerlySerializedAs("destroyedMaterial")] [Required][SerializeField] Material offMaterial;

    [SerializeField] Vector2 flickerPeriod = new Vector2(0.05f, 1f);
    
    bool broken = false;
    
    float nextFlickerTime;
    public bool LightIsOn => light.enabled;

    public Health Health => new(1);


    void Update()
    {
        TryFlicker();
    }

    void TryFlicker()
    {
        if (PowerLevel != PowerLevel.MinimalPower){
            return;
        }
        if (Time.time < nextFlickerTime){
            return;
        }
        nextFlickerTime = Time.time + General.RandomRange(flickerPeriod);
        if (LightIsOn){
            LightOff();
        }
        else{
            LightOn();
        }
    }


    void Break()
    {
        broken = true;
        gameObject.SetActive(false);
        meshRenderer.material = offMaterial;
    }

    public void Damage(Damage damage)
    {
        Break();
    }
    
    public override void SetPower(PowerLevel power)
    {
        base.SetPower(power);
        if (broken){
            return;
        }
        switch (power){
            case PowerLevel.FullPower:
                LightOn();
                break;
            case PowerLevel.MinimalPower:
                LightOn();
                break;
            case PowerLevel.NoPower:
                LightOff();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(power), power, null);
        }
    }

    void LightOff()
    {
        light.enabled = false;
        meshRenderer.material = offMaterial;
    }

    void LightOn()
    {
        meshRenderer.material = defaultMaterial;
        light.enabled = true;
    }
}