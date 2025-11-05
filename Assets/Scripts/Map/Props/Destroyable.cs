using UnityEngine;

public class Destroyable : MonoBehaviour, IDamagable
{
    [SerializeField] Health health;

    [SerializeField] GameObject gameObjectToDestroy;

    public GameObject GameObjectToDestroy => gameObjectToDestroy != null ? gameObjectToDestroy : gameObject;
    public Health Health => health;

    public void Damage(Damage damage)
    {
        if (!health.Alive){
            return;
        }
        health.Damage(damage);
        if (!health.Alive){
            Die();
        }
    }

    public void Die()
    {
        GameStats.i.OnObjectDestroyed(this);
        Destroy(GameObjectToDestroy);
    }
}