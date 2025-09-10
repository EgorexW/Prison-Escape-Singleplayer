using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class LinkedItems : MonoBehaviour, ITrap
{
    [BoxGroup("References")][Required][SerializeField] ObjectsFactory lines;
    [BoxGroup("References")] [Required] [SerializeField] Transform trap;
    
    Item[] items;


    public void Activate()
    {
        items = General.GetObjectRoot(transform).GetComponentsInChildren<Item>();
        lines.SetCount(items.Length);
        for (int i = 0; i < items.Length; i++){
            var item = items[i];
            item.onPickUp.AddListener(OnItemPicked);
            lines.GetObject(i).GetComponent<LineRenderer>().SetPositions(new Vector3[]{item.transform.position, trap.transform.position});
        }
    }
    void OnItemPicked(Item arg0)
    {
        foreach (var item in items){
            if (item != arg0){
                Destroy(item.gameObject);
            }
        }
        lines.SetCount(0);
    }
}
