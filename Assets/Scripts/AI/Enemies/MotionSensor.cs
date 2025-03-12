using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour, IDamagable, IElectric
{
    [SerializeField] [Required] GameObject laser;

    [SerializeField] float empResistance = 1;

    [SerializeField] bool working = true;
    
    public Health Health => new Health(1);

    public UnityEvent onActivation;

    public void Damage(Damage damage)
    {
        laser.SetActive(false);
        working = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Activate();
    }

    void Activate()
    {
        if (!working){
            return;
        }
        onActivation.Invoke();
    }
    
    public void EmpHit(float strenght)
    {
        laser.SetActive(false);
        General.CallAfterSeconds(() => laser.SetActive(true), strenght/empResistance);
    }
}
