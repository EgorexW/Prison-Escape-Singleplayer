using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Keycard : MonoBehaviour
{
    [BoxGroup("References")] [Required] public AccessLevel accessLevel;

    public bool oneUse;
    [FormerlySerializedAs("hackChance")] public Optional<float> hackStrenght;
    public bool ignoreDetectorOverlay;

    public bool ReadKeycard(AccessLevel requestedAccessLevel)
    {
        return accessLevel.HasAccess(requestedAccessLevel);
    }
}