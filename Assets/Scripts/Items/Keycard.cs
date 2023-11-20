using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : ItemBase, IKeycard
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
