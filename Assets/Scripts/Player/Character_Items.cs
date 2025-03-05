public partial class Character
{
    const int ThrowPower = 10;
    Inventory inventory;
    Item equipedItem = null;

    public void PickupItem(Item item){
        if (!inventory.CanAddItem()){
            return;
        }
        inventory.AddItem(item);
        item.OnPickup(this);
        if (equipedItem == null){
            EquipItem(item);
        }
    }
    public void RemoveItem(Item item){
        if (equipedItem == item){
            DeequipItem();
        }
        inventory.RemoveItem(item);
    }
    public void DropItem(Item item = null){
        if (item == null){
            item = GetHeldItem();
            if (item == null){
                return;
            }
        }
        RemoveItem(item);
        item.OnDrop(this, aim.forward * ThrowPower);
    }
    public void EquipItem(Item item)
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
    public Item GetHeldItem()
    {
        return equipedItem;
    }
    public Inventory GetInventory()
    {
        return inventory;
    }
}
