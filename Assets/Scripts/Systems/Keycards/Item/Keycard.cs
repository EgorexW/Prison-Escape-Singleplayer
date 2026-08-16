using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Keycard : MonoBehaviour
{
    [BoxGroup("References")] [Required] AccessLevel accessLevel;

    KeycardStatus status = KeycardStatus.Permanent;
    [FormerlySerializedAs("hackChance")] public Optional<float> hackStrenght;
    public bool ignoreDetectorOverlay;
    
    public KeycardStatus Status => status;
    public AccessLevel AccessLevel => accessLevel;
    public bool OneUse => status == KeycardStatus.UseActive || status == KeycardStatus.UseInactive;

    [FoldoutGroup("Events")] public UnityEvent<Keycard> onChanged = new();

    public bool ReadKeycard(AccessLevel requestedAccessLevel)
    {
        if (status == KeycardStatus.UseInactive){
            return false;
        }
        return AccessLevel.HasAccess(requestedAccessLevel);
    }

    public void SetStatus(KeycardStatus statusTmp){
        this.status = statusTmp;
        onChanged.Invoke(this);
    }
    
    public void SetAccessLevel(AccessLevel accessLevelTmp){
        this.accessLevel = accessLevelTmp;
        onChanged.Invoke(this);
    }
    
    public void OnAccessGranted(){
        if (status == KeycardStatus.UseActive){
            SetStatus(KeycardStatus.UseInactive);
        }
    }
}

public enum KeycardStatus{
    Permanent,
    UseActive,
    UseInactive
}