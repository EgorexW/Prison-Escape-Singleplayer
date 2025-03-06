using UnityEngine;

public class CelingLight : MonoBehaviour, IDamagable
{
    [SerializeField] MeshRenderer meshRenderer;

    [SerializeField] Material destroyedMaterial;
    
    public void Damage(Damage damage)
    {
        gameObject.SetActive(false);
        meshRenderer.material = destroyedMaterial;
    }

    public void Heal(Damage damage)
    {
        
    }

    public Health GetHealth()
    {
        return new Health();
    }
}
