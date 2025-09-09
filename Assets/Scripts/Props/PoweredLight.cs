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

    [SerializeField] bool onWithMinimalPower = false;
    [SerializeField] Health health;
    
    bool broken = false;

    public Health Health => health;


    public void Damage(Damage damage)
    {
        Die();
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
                LightWeak();
                break;
            case PowerLevel.NoPower:
                LightOff();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(power), power, null);
        }
    }

    void LightWeak()
    {
        if (onWithMinimalPower){
            LightOn();
        }
        else{
            LightOff();
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