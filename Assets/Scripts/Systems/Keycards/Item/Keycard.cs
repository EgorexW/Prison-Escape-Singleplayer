using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Keycard : MonoBehaviour, ISerializationCallbackReceiver
{
    [BoxGroup("References")] [Required] public AccessLevel accessLevel;

    public KeycardStatus status = KeycardStatus.Permanent;
    [FormerlySerializedAs("hackChance")] public Optional<float> hackStrenght;
    public bool ignoreDetectorOverlay;
    
    public bool OneUse => status == KeycardStatus.UseActive || status == KeycardStatus.UseInactive;

    public bool ReadKeycard(AccessLevel requestedAccessLevel)
    {
        if (status == KeycardStatus.UseInactive){
            return false;
        }
        return accessLevel.HasAccess(requestedAccessLevel);
    }
    
// 1. Keep the old serialized field hidden from Odin/Inspector
    [HideInInspector]
    [SerializeField]
    private bool useCase;

    // 2. Track whether migration is required
    [HideInInspector]
    [SerializeField]
    private bool isMigrated = false;
    
    public void OnBeforeSerialize() { }

    public void OnAfterDeserialize()
    {
        // 3. Migrate only once when legacy data exists
        if (!isMigrated)
        {
            // Define your bool-to-enum mapping logic:
            status = useCase ? KeycardStatus.UseActive : KeycardStatus.Permanent;
            isMigrated = true;
        }
    }

    public void OnAccessGranted(){
        if (status == KeycardStatus.UseActive){
            status = KeycardStatus.UseInactive;
        }
    }
}

public enum KeycardStatus{
    Permanent,
    UseActive,
    UseInactive
}