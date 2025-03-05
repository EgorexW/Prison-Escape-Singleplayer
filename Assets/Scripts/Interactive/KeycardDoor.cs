using UnityEngine;

public class KeycardDoor : Door
{
    [SerializeField] AccessLevel accessLevel;

    public override bool CanCharacterUse(Character character, bool onInteract)
    {
        Item item = character.GetHeldItem();
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
