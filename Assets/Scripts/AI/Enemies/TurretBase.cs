using System;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Shooting))]
[RequireComponent(typeof(TargetsSeeing))]
public class TurretBase : MonoBehaviour
{
    [SerializeField][GetComponent] TargetsSeeing seeing;
    [GetComponent][SerializeField] protected Shooting shooting;
    [SerializeField][Required] Transform shootPoint;
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float defaultRotationSpeed = 0.5f;
    [SerializeField] float angleToStartShooting = 0.1f;
    [SerializeField] bool defaultToStartRotation;
    
    [ShowIfInType(type = typeof(TurretBase))] public List<GameObject> targets;
    
    State state = State.Idle;
    GameObject currentTarget;
    Quaternion startRotation;

    protected enum State
    {
        Idle,
        Aiming
    }

    protected void Awake()
    {
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (state == TurretBase.State.Idle){
            CheckForTargets();
            RotateToDefault();
        }
        if (state == TurretBase.State.Aiming) Aim();
    }

    void RotateToDefault()
    {
        if (!defaultToStartRotation){
            return;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, Time.deltaTime * rotationSpeed * defaultRotationSpeed);
    }

    void CheckForTargets()
    {
        GameObject foundTarget = null;
        foreach (var target in targets){
            var result = seeing.CheckTargetVisibility(target);
            if (result > 0){
                foundTarget = target;
            }
        }
        if (foundTarget != null){
            StartAiming(foundTarget);
        }
    }

    protected virtual void StartAiming(GameObject foundTarget)
    {
        state = TurretBase.State.Aiming;
        currentTarget = foundTarget;
        Aim();
    }

    void Aim()
    {
        var targetVisibility = seeing.CheckTargetVisibility(currentTarget);
        if (targetVisibility <= 0){
            state = TurretBase.State.Idle;
            return;
        }
        var direction = currentTarget.transform.position - transform.position;
        direction.y = 0;
        var targetRotation = Quaternion.LookRotation(direction);
        if (Vector3.Angle(direction, transform.forward) < angleToStartShooting){
            Shoot();
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed * targetVisibility);
    }

    void Shoot()
    {
        Ray ray = new Ray(shootPoint.position, transform.forward);
        shooting.Shoot(ray);
    }
}