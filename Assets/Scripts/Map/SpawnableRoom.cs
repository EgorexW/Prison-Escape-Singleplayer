using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableRoom : MonoBehaviour
{
    [SerializeField] Transform joinPoint;
    [SerializeField] Transform dirPoint;

    public void SetPos(Vector3 joinPos, Vector3 dir){
        SetJoinPos(joinPos);
        SetDir(dir);
        SetJoinPos(joinPos);
    }
    void SetJoinPos(Vector3 joinPos){
        transform.position = joinPos - (joinPoint.position - transform.position);
    }
    void SetDir(Vector3 dir){
        Vector3 currentDir = dirPoint.position - joinPoint.position;
        Quaternion rotation = Quaternion.FromToRotation(currentDir, dir);;
        transform.rotation *= rotation;
    }
}
