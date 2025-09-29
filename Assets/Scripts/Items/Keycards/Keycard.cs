using UnityEngine;

public class Keycard : MonoBehaviour, IKeycard
{
    public AccessLevel accessLevel;
    
    public bool oneUse = false;
    
    public bool OneUse => oneUse;

    public bool HasAccess(AccessLevel requestedAccessLevel)
    {
        return accessLevel.HasAccess(requestedAccessLevel);
    }
}