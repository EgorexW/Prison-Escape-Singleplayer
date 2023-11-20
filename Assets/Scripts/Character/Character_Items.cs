using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character
{
    const int ThrowPower = 10;
    IInventory inventory;
    IItem equipedItem = null;

    public void PickupItem(IItem item){
        if (!inventory.CanAddItem()){
            return;
        }
        inventory.AddItem(item);
        item.OnPickup(this);
        if (equipedItem == null){
            EquipItem(item);
        }
    }
    public void RemoveItem(IItem item){
        if (equipedItem == item){
            DeequipItem();
        }
        inventory.RemoveItem(item);
    }
    public void DropItem(IItem item = null){
        if (item == null){
            item = GetHeldItem();
            if (item == null){
                return;
            }
        }
        RemoveItem(item);
        item.OnDrop(this, aim.forward * ThrowPower);
    }
    public void EquipItem(IItem item)
    {
        DeequipItem();
        equipedItem = item;
        equipedItem.OnEquip(this);
    }
    public void DeequipItem()
    {
        if (equipedItem == null){
            return;
        }
        equipedItem.OnDeequip(this);
        equipedItem = null;
    }
    public void DropItem(){
        DropItem(equipedItem);
    }
    public void UseHeldItem(bool alternative = false){
        if (equipedItem == null){
            return;
        }
        equipedItem.Use(this, alternative);
    }
    public void HoldUseHeldItem(bool alternative = false){
        if (equipedItem == null){
            return;
        }
        equipedItem.HoldUse(this, alternative);
    }
    public void StopUseHeldItem(bool alternative = false){
        if (equipedItem == null){
            return;
        }
        equipedItem.StopUse(this, alternative);
    }
    public IItem GetHeldItem()
    {
        return equipedItem;
    }
    public IInventory GetInventory()
    {
        return inventory;
    }
}
