using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public partial class Player
{
    const int THROW_POWER = 10;
    const float CONTINUOUS_COLLISION_DETECTION_TIME_ON_THROW = 2f;
    [SerializeField] [Required] Transform itemSlot;

    [FoldoutGroup("Events")] public UnityEvent onInventoryChange = new();
    [FoldoutGroup("Events")] public UnityEvent onSwapItem = new();
    [FoldoutGroup("Events")] public UnityEvent onThrowItem = new();
    [FoldoutGroup("Events")] public UnityEvent onUseItem = new();

    [FoldoutGroup("Events")] public UnityEvent onPickUpItem = new();

    [ShowInInspector] Item equipedItem;

    Inventory inventory;

    public void PickupItem(Item item)
    {
        if (!inventory.CanAddItem()){
            ThrowItem();
            if (!inventory.CanAddItem()){
                Debug.LogWarning("Discarded item, but still can't add", this);
                return;
            }
        }
        onPickUpItem.Invoke();
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

    public void RemoveItem(Item item)
    {
        if (equipedItem == item){
            DeequipItem();
        }
        inventory.RemoveItem(item);
        item.gameObject.SetActive(true);
        item.transform.SetParent(null); // Detach from character
        item.Rigidbody.isKinematic = false; // Enable physics
    }

    public void ThrowItem(Item item)
    {
        if (item == null){
            Debug.LogWarning("Can't throw null item", this);
            return;
        }

        item.StopUse(this);
        RemoveItem(item);

        var force = aim.forward * THROW_POWER;
        item.Rigidbody.AddForce(force, ForceMode.Impulse); // Add force for throwing
        item.Rigidbody.collisionDetectionMode =
            CollisionDetectionMode.Continuous; // Enable continuous collision detection

        // Reset collision detection mode after a delay
        General.CallAfterSeconds(() => { item.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete; },
            CONTINUOUS_COLLISION_DETECTION_TIME_ON_THROW);

        onThrowItem.Invoke();
    }

    public void EquipItem(Item item)
    {
        DeequipItem();
        equipedItem = item;
        equipedItem.gameObject.SetActive(true);
        onInventoryChange.Invoke();
    }

    public void DeequipItem()
    {
        if (equipedItem == null){
            return;
        }
        equipedItem.StopUse(this);
        equipedItem.gameObject.SetActive(false);
        equipedItem = null;
        onInventoryChange.Invoke();
    }

    public void ThrowItem()
    {
        ThrowItem(equipedItem);
    }

    public void UseHeldItem(bool alternative = false)
    {
        if (equipedItem == null){
            return;
        }
        equipedItem.Use(this, alternative);
        onUseItem.Invoke();
    }

    public void HoldUseHeldItem(bool alternative = false)
    {
        if (equipedItem == null){
            return;
        }
        equipedItem.HoldUse(this, alternative);
        onUseItem.Invoke();
    }

    public void StopUseHeldItem(bool alternative = false)
    {
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

    public void SwapItem(int changeIndex = 1)
    {
        var items = GetInventory().GetItems();
        if (items.Count == 0){
            return;
        }
        var index = items.IndexOf(GetHeldItem());
        index += changeIndex;
        while (index < 0) index += items.Count;
        while (index >= items.Count) index -= items.Count;
        EquipItem(items[index]);
        onSwapItem.Invoke();
    }
}