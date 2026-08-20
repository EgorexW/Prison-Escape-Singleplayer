using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MetalDetector : PoweredDevice
{
    [SerializeField] float cooldown = 15;
    
    float lastStealTime = -Mathf.Infinity;
    
    [FoldoutGroup("Events")] public UnityEvent beep;

    void Update()
    {
        if (!IsPowered()){
            return;
        }
        if (lastStealTime + cooldown <= Time.time && lastStealTime + cooldown > Time.time - Time.deltaTime){
            // Debug.Log("Metal detector is ready to steal again", this);
            beep.Invoke();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!IsPowered()){
            return;
        }
        if (Time.time - lastStealTime < cooldown){
            // Debug.Log("Metal detector on cooldown", this);
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
        beep.Invoke();
        lastStealTime = Time.time;
    }
}