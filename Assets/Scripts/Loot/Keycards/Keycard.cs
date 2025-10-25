using Sirenix.OdinInspector;
using UnityEngine;

public class Keycard : MonoBehaviour, IKeycard
{
    [BoxGroup("References")] [Required] public AccessLevel accessLevel;

    public bool oneUse;

    public bool OneUse => oneUse;

    public bool HasAccess(AccessLevel requestedAccessLevel)
    {
        return accessLevel.HasAccess(requestedAccessLevel);
    }
}