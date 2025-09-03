using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour
{
    public UnityEvent onActivation;

    void OnTriggerEnter(Collider other)
    {
        onActivation.Invoke();
    }
}
