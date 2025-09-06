using UnityEngine;
using UnityEngine.Serialization;

public class Destroyable : MonoBehaviour, IDamagable
{
    [SerializeField] Health health;
    
    public Health Health => health;

    public void Die()
    {
        Destroy(gameObject);
    }
}
