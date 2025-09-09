using System.Collections.Generic;
using UnityEngine;

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
        if (ActivateDisk(disc)){
            player.RemoveItem(item);
            Destroy(item.gameObject);
        }
        else{
            Debug.LogWarning("Disk not activated: " + disc.name, disc);
        }
    }

    public float HoldDuration => 3f;

    bool ActivateDisk(Disc disc)
    {
        var activated = false;
        foreach (var handler in linkedHandlers)
            if (handler.CanHandleDisk(disc)){
                activated = true;
                handler.HandleDisk(disc);
            }
        return activated;
    }
}

interface IDiskHandler
{
    bool CanHandleDisk(Disc disc);
    void HandleDisk(Disc disc);
}