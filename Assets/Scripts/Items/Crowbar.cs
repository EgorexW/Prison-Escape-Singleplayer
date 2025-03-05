using UnityEngine;

public class Crowbar : Item, IKeycard
{
    [SerializeField] AccessLevel accessLevel;

    public override void Use(Character character, bool alternative = false){
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
