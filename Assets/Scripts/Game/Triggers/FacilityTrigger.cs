using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class FacilityTrigger : MonoBehaviour
{
    public string id;

    [FoldoutGroup("Events")] public UnityEvent onActivate;

    public virtual void Activate()
    {
        onActivate.Invoke();
    }
}