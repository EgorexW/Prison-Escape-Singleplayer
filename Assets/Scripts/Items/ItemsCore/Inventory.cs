using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    List<Item> items = new();
    [SerializeField] int size = 1;
    UnityEvent onInventoryChange = new();
    public UnityEvent OnInventoryChange => onInventoryChange;

    public bool CanAddItem(){
        return items.Count < size;
    }
    public void AddItem(Item item)
    {
        if (!CanAddItem()){
            return;
        }
        item.isHeld = true;
        items.Add(item);
        onInventoryChange.Invoke();
    }
    public List<Item> GetItems()
    {
        return new(items);
    }

    public void RemoveItem(Item item)
    {
        item.isHeld = false;
        items.Remove(item);
        onInventoryChange.Invoke();
    }
    public int GetSize(){
        return size;
    }
}
