using Sirenix.OdinInspector;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    [BoxGroup("References")] [Required] public AccessLevel accessLevel;

    public bool oneUse;
    public Optional<float> hackChance;

    public bool ReadKeycard(AccessLevel requestedAccessLevel)
    {
        return accessLevel.HasAccess(requestedAccessLevel);
    }
}