using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] 
    [PropertyRange(0.1f, 20f)]
    public Vector3 areaSize = new Vector3(5, 5, 5);

    [SerializeField] Damage damage;
    
    public void Activate()
    {
        Collider[] objectsInArea = Physics.OverlapBox(transform.position, areaSize / 2);
        HashSet<IDamagable> damagablesHit = new HashSet<IDamagable>();
        foreach (Collider obj in objectsInArea)
        {
            var damagable = General.GetRootComponent<IDamagable>(obj.transform, false);
            if (!damagablesHit.Add(damagable)) continue;
            damagable?.Damage(damage);
        }
        gameObject.SetActive(false);
    }
}
