using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CelingLight : MonoBehaviour, IDamagable, IPowerSystemDevice
{
    [Required][SerializeField] MeshRenderer meshRenderer;

    [Required][SerializeField] Material defaultMaterial;
    [FormerlySerializedAs("destroyedMaterial")] [Required][SerializeField] Material offMaterial;
    
    [FormerlySerializedAs("chanceToStartBroken")] [SerializeField] float chanceToStartUnpowered = 0.1f;
    [SerializeField] float empResistance = 0.5f;

    State state = State.Powered;
    
    void Start()
    {
        General.GetRootComponent<PowerSystem>(gameObject).AddDevice(this);
        if (Random.value < chanceToStartUnpowered){
            SetPower(false);
        }
    }

    void Break()
    {
        state = State.Broken;
        gameObject.SetActive(false);
        meshRenderer.material = offMaterial;
    }

    public void Damage(Damage damage)
    {
        Break();
    }

    public Health Health => new(1);
    public void SetPower(bool power)
    {
        if (state == State.Broken){
            return;
        }
        if (power){
            state = State.Powered;
            meshRenderer.material = defaultMaterial;
            gameObject.SetActive(true);
        }
        else{
            state = State.Unpowered;
            gameObject.SetActive(false);
            meshRenderer.material = offMaterial;
        }
    }

    public void EmpHit(float strenght)
    {
        SetPower(false);
        General.CallAfterSeconds(() => SetPower(true), strenght/empResistance);
    }
}

enum State
{
    Powered,
    Unpowered,
    Broken
}