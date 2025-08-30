using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class KeycardReader : MonoBehaviour, IInteractive
{
    [BoxGroup("References")][Required][SerializeField] AccessLevel accessLevel;
    [BoxGroup("References")][Required][SerializeField] DoorInteract doorToUnlock;
    
    [SerializeField] KeycardReaderEffects effects;

    void Awake()
    {
        doorToUnlock.unlocked = false;
    }

    public void Interact(Player player)
    {
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