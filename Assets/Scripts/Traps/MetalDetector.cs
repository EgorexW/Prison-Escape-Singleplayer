using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MetalDetector : PoweredDevice
{
[FoldoutGroup("Events")] public UnityEvent<Item> onItemTaken;
    
    public void OnTriggerEnter(Collider other)
    {
        if (!IsPowered()){
            return;
        }
        var gameObject = other.gameObject;
        if (other.attachedRigidbody != null){
            gameObject = other.attachedRigidbody.gameObject;
        }
        var item = gameObject.GetComponent<Item>();
        if (item != null && !item.isHeld){
            TakeItem(item);
            return;
        }
        var player = gameObject.GetComponent<Player>();
        if (player != null){
            if (player.GetInventory().GetItems().Count == 0){
                return;
            }
            var randomItem = player.GetInventory().GetItems().Random();
            player.RemoveItem(randomItem);
            TakeItem(randomItem);
        }
    }

    void TakeItem(Item item)
    {
        var facilityTrigger = FacilityTriggers.GetSwitch("Confiscated Items");
        if (facilityTrigger == null){
            Debug.LogWarning("No facility trigger named 'Confiscated Items' found, destroying item", this);
            Destroy(item);
            return;
        }
        var pos = facilityTrigger.transform.position;
        item.transform.position = pos;
        item.Rigidbody.linearVelocity = Vector3.zero;
        onItemTaken.Invoke(item);
    }
}