using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class PoweredLight : PoweredDevice, IDamagable
{
    [Required][SerializeField] MeshRenderer meshRenderer;
    [GetComponent] [SerializeField] new Light light;

    [Required][SerializeField] Material defaultMaterial;
    [FormerlySerializedAs("destroyedMaterial")] [Required][SerializeField] Material offMaterial;

    [SerializeField] Vector2 flickerPeriod = new Vector2(0.05f, 1f);
    [SerializeField] Health health;
    
    bool broken = false;
    
    float nextFlickerTime;
    public bool LightIsOn => light.enabled;

    public Health Health => health;


    void Update()
    {
        TryFlicker();
    }

    void TryFlicker()
    {
        if (GetPowerLevel() != PowerLevel.MinimalPower){
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


    public void Die()
    {
        broken = true;
        gameObject.SetActive(false);
        meshRenderer.material = offMaterial;
    }

    protected override void OnPowerChanged()
    {
        base.OnPowerChanged();
        if (broken){
            return;
        }
        var power = GetPowerLevel();
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