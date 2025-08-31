using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class KeycardReader : PoweredDevice, IInteractive
{
    [BoxGroup("References")][Required][SerializeField] public AccessLevel accessLevel;
    [BoxGroup("References")][Required][SerializeField] DoorInteract doorToUnlock;
    
    [SerializeField] KeycardReaderEffects effects;

    void Awake()
    {
        doorToUnlock.unlocked = false;
        if (effects != null){
            effects.keycardReader = this;
        }
    }

    public void Interact(Player player)
    {
        if (GetPowerLevel() == PowerLevel.NoPower){
            return;
        }
        var item = player.GetHeldItem();
        if (item is not IKeycard keycard || !keycard.HasAccess(accessLevel)){
            effects?.AccessDenied();
            return;
        }
        effects?.AccessGranted();
        doorToUnlock.unlocked = true;
    }

    public float HoldDuration => 1;
}