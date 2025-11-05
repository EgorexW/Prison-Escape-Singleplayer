using Sirenix.OdinInspector;
using UnityEngine;

public class LinkedLoot : MonoBehaviour, ITrap
{
    [BoxGroup("References")] [Required] [SerializeField] ObjectsFactory lines;
    [BoxGroup("References")] [Required] [SerializeField] Transform trap;

    Loot[] loots;


    public void Activate()
    {
        loots = General.GetObjectRoot(transform).GetComponentsInChildren<Loot>();
        lines.SetCount(loots.Length);
        for (var i = 0; i < loots.Length; i++){
            var item = loots[i];
            item.onInteract.AddListener(OnItemPicked);
            lines.GetObject(i).GetComponent<LineRenderer>()
                .SetPositions(new[]{ item.transform.position, trap.transform.position });
        }
    }

    void OnItemPicked(Loot loot)
    {
        foreach (var item in loots)
            if (item != loot){
                Destroy(item.gameObject);
            }
        lines.SetCount(0);
        loot.onInteract.RemoveListener(OnItemPicked);
    }
}