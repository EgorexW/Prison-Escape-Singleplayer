using UnityEngine;

public class Keycard : Item, IKeycard
{
    [SerializeField] AccessLevel accessLevel;

    public bool HasAccess(AccessLevel requestedAccessLevel)
    {
        return accessLevel.HasAccess(requestedAccessLevel);
    }

    public bool CanOpenWhenHeld()
    {
        return true;
    }
}
