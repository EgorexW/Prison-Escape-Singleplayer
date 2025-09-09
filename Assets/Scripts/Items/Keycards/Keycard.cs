using UnityEngine;

public class Keycard : MonoBehaviour, IKeycard
{
    [SerializeField] AccessLevel accessLevel;

    public bool HasAccess(AccessLevel requestedAccessLevel)
    {
        return accessLevel.HasAccess(requestedAccessLevel);
    }
}