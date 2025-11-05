using System.Collections.Generic;
using UnityEngine;

public class DiscHandler : PoweredDevice, IInteractive
{
    List<IDiscHandler> linkedHandlers;

    void Awake()
    {
        linkedHandlers = new List<IDiscHandler>(GetComponentsInChildren<IDiscHandler>());
    }

    public void Interact(Player player)
    {
        if (!IsPowered()){
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
            Debug.LogWarning("Disk not activated: " + disc, disc);
        }
    }

    public float HoldDuration => 3f;

    bool ActivateDisk(Disc disc)
    {
        var activated = false;
        foreach (var handler in linkedHandlers)
            if (handler.CanHandleDisc(disc)){
                activated = true;
                handler.HandleDisc(disc);
            }
        return activated;
    }
}

interface IDiscHandler
{
    bool CanHandleDisc(Disc disc);
    void HandleDisc(Disc disc);
}