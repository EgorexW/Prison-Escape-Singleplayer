using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KeycardDetector : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] KeycardReader keycardReader;
    
    public void OnTriggerEnter(Collider other)
    {
        var keycard = General.GetComponentFromCollider<Keycard>(other);
        if (keycard != null){
            // keycardReader.ReadKeycard(keycard);
        }
    }
}
