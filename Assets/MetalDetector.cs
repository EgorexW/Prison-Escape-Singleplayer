using UnityEngine;

public class MetalDetector : PoweredDevice
{
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
        // Can stash them somewhere in the future
        var pos = FacilityObjects.GetSwitch("Confiscated Items").transform.position; // TODO make it more error proof
        item.transform.position = pos;
        item.Rigidbody.linearVelocity = Vector3.zero;
    }
}
