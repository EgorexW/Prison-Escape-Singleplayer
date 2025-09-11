using Sirenix.OdinInspector;
using UnityEngine;

public class LinkedItems : MonoBehaviour, ITrap
{
    [BoxGroup("References")] [Required] [SerializeField] ObjectsFactory lines;
    [BoxGroup("References")] [Required] [SerializeField] Transform trap;

    Item[] items;


    public void Activate()
    {
        items = General.GetObjectRoot(transform).GetComponentsInChildren<Item>();
        lines.SetCount(items.Length);
        for (var i = 0; i < items.Length; i++){
            var item = items[i];
            item.onPickUp.AddListener(OnItemPicked);
            lines.GetObject(i).GetComponent<LineRenderer>()
                .SetPositions(new[]{ item.transform.position, trap.transform.position });
        }
    }

    void OnItemPicked(Item arg0)
    {
        foreach (var item in items)
            if (item != arg0){
                Destroy(item.gameObject);
            }
        lines.SetCount(0);
    }
}