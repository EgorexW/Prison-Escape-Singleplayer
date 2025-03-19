using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AIChaser : MonoBehaviour
{
    [Required] [SerializeField] TurretBase turretBase;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float targetDis = 5f;
    [SerializeField] float rotationSpeed = 90;
    
    GameObject target;

    public void Init(GameObject target)
    {
        this.target = target;
        turretBase.targets = new List<GameObject>(){target};
    }

    protected void FixedUpdate()
    {
        if (target == null){
            return;
        }
        var targetDir = target.transform.position - transform.position;
        if (targetDir.magnitude > targetDis){
            transform.position += targetDir.normalized * moveSpeed * Time.fixedDeltaTime;
        }
        targetDir.y = 0;
        var targetRotation = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,  Time.fixedDeltaTime * rotationSpeed);
    }
}
