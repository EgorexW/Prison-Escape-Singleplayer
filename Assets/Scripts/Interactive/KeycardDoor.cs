using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoor : Door
{
    [SerializeField] AccessLevel accessLevel;

    public override bool CanCharacterUse(ICharacter character, bool onInteract)
    {
        IItem item = character.GetHeldItem();
        if (item is not IKeycard){
            return false;
        }
        IKeycard keycard = (IKeycard)item;
        if (onInteract)
        {
            if (!keycard.CanOpenWhenHeld()){
                return false;
            }
        }
        return keycard.HasAccess(accessLevel);
    }
}
