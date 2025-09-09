using UnityEngine;

public class Destroyable : MonoBehaviour, IDamagable
{
    [SerializeField] Health health;

    [SerializeField] GameObject gameObjectToDestroy;
    public Health Health => health;

    public void Damage(Damage damage)
    {
        Health.Damage(damage);
        if (!Health.Alive){
            Die();
        }
    }

    public void Die()
    {
        if (gameObjectToDestroy != null){
            Destroy(gameObjectToDestroy);
            return;
        }
        Destroy(gameObject);
    }
}