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
        throw new System.NotImplementedException();
    }

    public Health GetHealth()
    {
        return new Health();
    }
}
