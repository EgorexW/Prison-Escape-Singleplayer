using UnityEngine;
using UnityEngine.Serialization;

public class Destroyable : MonoBehaviour, IDamagable
{
    [SerializeField] Health health;
    
    public Health Health => health;
    public void Damage(Damage damage)
    {
        health.Damage(damage);
        if (health.health <= 0){
            Destroy(gameObject);
        }
    }
}
