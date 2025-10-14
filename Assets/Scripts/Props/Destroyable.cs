using UnityEngine;

public class Destroyable : MonoBehaviour, IDamagable
{
    [SerializeField] Health health;

    [SerializeField] GameObject gameObjectToDestroy;

    public GameObject GameObjectToDestroy => gameObjectToDestroy != null ? gameObjectToDestroy : gameObject;
    public Health Health => health;

    public void Damage(Damage damage)
    {
        health.Damage(damage);
        if (!health.Alive){
            Die();
        }
    }

    public void Die()
    {
        Destroy(GameObjectToDestroy);
    }
}