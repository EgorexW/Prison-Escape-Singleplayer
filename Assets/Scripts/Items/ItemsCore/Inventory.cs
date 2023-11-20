using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour, IInventory
{
    List<IItem> items = new();
    [SerializeField] int size = 1;
    UnityEvent onInventoryChange = new();
    public UnityEvent OnInventoryChange => onInventoryChange;

    public bool CanAddItem(){
        return items.Count < size;
    }
    public void AddItem(IItem item = null)
    {
        if (items.Count >= size){
            return;
        }
        items.Add(item);
        onInventoryChange.Invoke();
    }
    public List<IItem> GetItems()
    {
        return new(items);
    }

    public void RemoveItem(IItem item)
    {
        items.Remove(item);
        onInventoryChange.Invoke();
    }
    public int GetSize(){
        return size;
    }
}
