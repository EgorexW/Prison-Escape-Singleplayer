using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Shooting))]
[RequireComponent(typeof(TargetsSeeing))]
public class Turret : MonoBehaviour, IDamagable, IAIObject, IElectric
{
    [SerializeField][GetComponent] TargetsSeeing seeing;
    [GetComponent][SerializeField] Shooting shooting;
    
    [SerializeField][Required] Transform shootPoint;

    [SerializeField] Health health;
    [SerializeField] float empResistance = 1;
    
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float angleToStartShooting = 0.1f;

    public Health Health => health;
    
    enum State
    {
        Idle,
        Aiming
    }
    
    
    State state = State.Idle;
    MainAI mainAI;

    List<GameObject> Targets => mainAI.Targets;
    
    GameObject currentTarget;

    void Update()
    {
        if (state == State.Idle) CheckForTargets();
        if (state == State.Aiming) Aim();
    }

    void CheckForTargets()
    {
        GameObject foundTarget = null;
        foreach (var target in Targets){
            var result = seeing.CheckTargetVisible(target);
            if (result){
                foundTarget = target;
            }
        }
        if (foundTarget != null){
            StartAiming(foundTarget);
        }
    }

    void StartAiming(GameObject foundTarget)
    {
        state = State.Aiming;
        currentTarget = foundTarget;
        Aim();
    }

    void Aim()
    {
        if (!seeing.CheckTargetVisible(currentTarget)){
            state = State.Idle;
            return;
        }
        var direction = currentTarget.transform.position - transform.position;
        direction.y = 0;
        var targetRotation = Quaternion.LookRotation(direction);
        if (Vector3.Angle(direction, transform.forward) < angleToStartShooting){
            Shoot();
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    void Shoot()
    {
        Ray ray = new Ray(shootPoint.position, transform.forward);
        shooting.Shoot(ray);
    }

    public void Damage(Damage damage)
    {
        health.Damage(damage);
        if (!health.Alive){
            gameObject.SetActive(false);
        }
    }

    public void Init(MainAI mainAI)
    {
        this.mainAI = mainAI;
    }

    public void EmpHit(float strenght)
    {
        enabled = false;
        General.CallAfterSeconds(() => enabled = true, strenght/empResistance);
    }
}