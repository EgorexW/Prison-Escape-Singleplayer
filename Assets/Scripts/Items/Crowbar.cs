using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : ItemBase, IKeycard
{
    [SerializeField] AccessLevel accessLevel;

    public override void Use(ICharacter character, bool alternative = false){
        IInteractive interactive = character.GetInteractive();
        if (interactive is not IDoor){
            return;
        }
        IDoor door = (IDoor)interactive;
        if (!door.CanCharacterUse(character, false)){
            return;
        }
        door.Open();
        door.LockState(true);
        character.RemoveItem(this);
    }
    public bool HasAccess(AccessLevel requestedAccessLevel)
    {
        return accessLevel.HasAccess(requestedAccessLevel);
    }

    public bool CanOpenWhenHeld()
    {
        return false;
    }
}
