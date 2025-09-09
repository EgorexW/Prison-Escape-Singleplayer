using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] GameObject laser;

    public UnityEvent onActivation;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")){
            return;
        } // TODO make it cleaner
        onActivation.Invoke();
    }

    public void SetActive(bool active)
    {
        laser.SetActive(active);
    }
}