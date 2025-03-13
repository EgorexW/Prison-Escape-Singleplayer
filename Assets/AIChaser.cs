using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AIChaser : MonoBehaviour
{
    [Required] [SerializeField] TurretBase turretBase;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float targetDis = 5f;
    
    GameObject target;

    public void Init(GameObject target)
    {
        this.target = target;
        turretBase.targets = new List<GameObject>(){target};
    }

    protected void Update()
    {
        var targetDir = target.transform.position - transform.position;
        if (targetDir.magnitude < targetDis){
            return;
        }
        transform.position += targetDir.normalized * moveSpeed;
    }
}
