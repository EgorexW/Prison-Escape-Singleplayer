using Sirenix.OdinInspector;
using UnityEngine;

public class DamagePropagator : MonoBehaviour, IDamagable
{
    [BoxGroup("References")] [Required] [SerializeField] Destroyable connectedDestroyable;

    public Health Health => connectedDestroyable.Health;

    public void Damage(Damage damage)
    {
        connectedDestroyable.Damage(damage);
    }
}