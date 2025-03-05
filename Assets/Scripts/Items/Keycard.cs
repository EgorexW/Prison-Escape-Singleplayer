using UnityEngine;

public class Keycard : Item, IKeycard
{
    [SerializeField] AccessLevel accessLevel;
    [SerializeField] Sprite sprite;

    public bool HasAccess(AccessLevel requestedAccessLevel)
    {
        return accessLevel.HasAccess(requestedAccessLevel);
    }

    public override Sprite GetPortrait()
    {
        return sprite;
    }

    public bool CanOpenWhenHeld()
    {
        return true;
    }
}
