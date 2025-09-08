using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DiscHandler : PoweredDevice, IInteractive
{
    List<IDiskHandler> linkedHandlers;

    void Awake()
    {
        linkedHandlers = new List<IDiskHandler>(GetComponentsInChildren<IDiskHandler>());
    }

    public void Interact(Player player)
    {
        if (GetPowerLevel() != PowerLevel.FullPower){
            return;
        }
        var item = player.GetHeldItem();
        var disc = item.GetComponent<Disc>();
        if (disc == null){
            // visuals?.AccessDenied();
            return;
        }
        // visuals?.AccessGranted();
        player.RemoveItem(item);
        Destroy(item.gameObject);
        ActivateDisk(disc);
    }

    public float HoldDuration => 3f;

    void ActivateDisk(Disc disc)
    {
        foreach (var handler in linkedHandlers){
            if (handler.CanHandleDisk(disc)){
                handler.HandleDisk(disc);
            }
        }
    }
}

interface IDiskHandler
{
    bool CanHandleDisk(Disc disc);
    void HandleDisk(Disc disc);
}