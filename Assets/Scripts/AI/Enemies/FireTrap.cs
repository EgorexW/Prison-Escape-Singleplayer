using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FireTrap : MonoBehaviour, IAIObject
{
    [SerializeField][Required] GameObject effectPrefab;
    
    [SerializeField] 
    [PropertyRange(0.1f, 20f)]
    public Vector3 areaSize = new Vector3(5, 5, 5);

    [SerializeField] Damage damage;
    
    // public AIObjectType aiType => AIObjectType.Trap;

    MainAI mainAI;

    public void Activate()
    {
        var playerMark = new PlayerMark(transform.position, 2);
        mainAI.PlayerNoticed(playerMark);
        Collider[] objectsInArea = Physics.OverlapBox(transform.position, areaSize / 2);
        var damagablesHit = General.GetUniqueRootComponents<IDamagable>(objectsInArea);
        var effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        effect.transform.localScale = areaSize;
        foreach (IDamagable damagable in damagablesHit)
        {
            damagable.Damage(damage);
        }
        gameObject.SetActive(false);
    }

    public void Init(MainAI mainAI)
    {
        this.mainAI = mainAI;
    }
}
