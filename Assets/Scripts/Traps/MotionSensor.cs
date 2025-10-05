using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] GameObject laser;

    public UnityEvent onActivation;

    void OnTriggerEnter(Collider other)
    {
        var player = General.GetComponentFromCollider<Player>(other);
        if (player == null){
            return;
        }
        onActivation.Invoke();
    }

    public void SetActive(bool active)
    {
        laser.SetActive(active);
    }
}