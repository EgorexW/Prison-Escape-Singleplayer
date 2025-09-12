using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class FacilityObject : MonoBehaviour
{
    public string id;

    [FoldoutGroup("Events")] public UnityEvent onActivate;

    public void Activate()
    {
        onActivate.Invoke();
    }
}