using System;
using System.Collections;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CeilingLight : PoweredDevice, IDamagable
{
    [GetComponent] [SerializeField] new Light light;
    [Required] [SerializeField] MeshRenderer meshRenderer;

    [Required] [SerializeField] Material defaultMaterial;
    [FormerlySerializedAs("destroyedMaterial")] [Required] [SerializeField] Material offMaterial;

    [SerializeField] bool onWithMinimalPower;
    [SerializeField] Health health;

    [BoxGroup("Flickering")][SerializeField] float flickerChancePerSecond = 0.01f;
    [BoxGroup("Flickering")][SerializeField] float flickerTime = 0.5f;
    [BoxGroup("Flickering")][SerializeField] float flickerFrequency = 0.01f;
    [BoxGroup("Flickering")][SerializeField] float flickerStrenght = 5;

    bool broken;
    float defaultIntensity;
    bool LightEnabled => light.enabled;

    public Health Health => health;

    void Awake(){
        defaultIntensity = light.intensity;
    }

    void Update(){
        if (!LightEnabled){
            return;
        }
        if (Time.time % 1 - Time.deltaTime < 0){
            if (Random.value < flickerChancePerSecond){
                Flicker();
            }
        }
    }

    void Flicker(){
        StartCoroutine(FlickerCoroutine());
    }

    IEnumerator FlickerCoroutine(){
        float time = flickerTime;
        while (time > 0){
            time -= Time.deltaTime;
            if (Random.value < flickerFrequency){
                light.intensity = Random.value * flickerStrenght;
            }
            yield return null;
        }
        light.intensity = defaultIntensity;
    }

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

    public void SetLight(Material material, Color lightColor)
    {
        defaultMaterial = material;
        light.color = lightColor;
        OnPowerChanged();
    }
}