using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class CelingLight : MonoBehaviour, IDamagable
{
    [Required][SerializeField] MeshRenderer meshRenderer;

    [Required][SerializeField] Material defaultMaterial;
    [Required][SerializeField] Material destroyedMaterial;
    
    [SerializeField] float chanceToStartBroken = 0.1f;

    void Start()
    {
        if (Random.value < chanceToStartBroken){
            Break();
        }
    }

    void Break()
    {
        gameObject.SetActive(false);
        meshRenderer.material = destroyedMaterial;
    }

    public void Damage(Damage damage)
    {
        Break();
    }

    public Health Health => new(1);
}