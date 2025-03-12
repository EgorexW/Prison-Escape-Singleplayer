using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public partial class Character
{
    [SerializeField][Required] Transform itemSlot;
    
    [ShowInInspector] Item equipedItem = null;

    [FoldoutGroup("Events")] public readonly UnityEvent onInventoryChange = new();
    
    const int THROW_POWER = 10;
    const float CONTINUOUS_COLLISION_DETECTION_TIME_ON_THROW = 2f;
    
    Inventory inventory;

    public void PickupItem(Item item){
        if (!inventory.CanAddItem()){
            return;
        }
        inventory.AddItem(item);
        
        item.gameObject.SetActive(false);
        item.Rigidbody.linearVelocity = Vector3.zero;
        item.Rigidbody.angularVelocity = Vector3.zero;
        item.Rigidbody.isKinematic = true;
        item.transform.SetParent(itemSlot);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        
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
    public void ThrowItem(Item item = null){
        if (item == null){
            item = GetHeldItem();
            if (item == null){
                return;
            }
        }
        RemoveItem(item);
        
        Vector3 force = aim.forward * THROW_POWER;
        item.gameObject.SetActive(true);
        item.transform.SetParent(null); // Detach from character
        item.Rigidbody.isKinematic = false; // Enable physics
        item.Rigidbody.AddForce(force, ForceMode.Impulse); // Add force for throwing
        item.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous; // Enable continuous collision detection

        // Reset collision detection mode after a delay
        General.CallAfterSeconds(() =>
        {
            item.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }, CONTINUOUS_COLLISION_DETECTION_TIME_ON_THROW);
    }
    public void EquipItem(Item item)
    {
        DeequipItem();
        equipedItem = item;
        equipedItem.gameObject.SetActive(true);
    }
    public void DeequipItem()
    {
        if (equipedItem == null){
            return;
        }
        equipedItem.StopUse(this);
        equipedItem.gameObject.SetActive(false);
        equipedItem = null;
    }
    public void ThrowItem(){
        ThrowItem(equipedItem);
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
