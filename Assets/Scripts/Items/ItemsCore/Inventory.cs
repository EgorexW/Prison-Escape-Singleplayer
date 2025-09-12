using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] int size = 1;
    readonly List<Item> items = new();
    public UnityEvent OnInventoryChange{ get; } = new();

    public bool CanAddItem()
    {
        return items.Count < size;
    }

    public void AddItem(Item item)
    {
        if (!CanAddItem()){
            return;
        }
        item.isHeld = true;
        items.Add(item);
        OnInventoryChange.Invoke();
    }

    public List<Item> GetItems()
    {
        return new List<Item>(items);
    }

    public void RemoveItem(Item item)
    {
        item.isHeld = false;
        items.Remove(item);
        OnInventoryChange.Invoke();
    }

    public int GetSize()
    {
        return size;
    }

    public void IncreaseSize(int by)
    {
        if (by <= 0){
            Debug.LogWarning("Inventory size increase must be positive", this);
            return;
        }
        size += by;
        OnInventoryChange.Invoke();
    }
}