using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInventory {
    void AddItem(IItem item = null);
    void RemoveItem(IItem item);
    List<IItem> GetItems();
    int GetSize();
    bool CanAddItem();

    UnityEvent OnInventoryChange{get;}
}