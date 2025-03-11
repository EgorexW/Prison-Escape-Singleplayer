using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour, IDamagable
{
    public Health Health => new Health(1);

    public UnityEvent onActivation;

    public void Damage(Damage damage)
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Activate();
    }

    void Activate()
    {
        onActivation.Invoke();
    }
}
