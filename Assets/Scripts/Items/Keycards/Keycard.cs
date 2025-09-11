using UnityEngine;

public class Keycard : MonoBehaviour, IKeycard
{
    [SerializeField] public AccessLevel accessLevel;

    public bool HasAccess(AccessLevel requestedAccessLevel)
    {
        return accessLevel.HasAccess(requestedAccessLevel);
    }
}