using UnityEngine;

class TargetsSeeing : MonoBehaviour
{
    [SerializeField] bool log;
    
    [SerializeField] [Range(0, 90)] float maxAngle = 45;
    [SerializeField] float maxDis = 15;
    public bool CheckTargetVisible(GameObject target)
    {
        var direction = target.transform.position - transform.position;
        var distance = direction.magnitude;
        if (distance > maxDis){
            if (log) Debug.Log("Target too far away");
            return false;
        }
        var angle = Vector3.Angle(direction, transform.forward);
        if (angle > maxAngle){
            if (log) Debug.Log("Target too far to the side");
            return false;
        }
        var ray = new Ray(transform.position, direction);
        if (!Physics.Raycast(ray, out var hit, distance)){
            if (log) Debug.Log("Raycast failed");
            return false;
        }
        if (log){
            Debug.Log("Hit Object:" + hit.transform.gameObject.name);
            Debug.Log("Target Object:" + target.name);
        }
        return hit.transform.gameObject == target;
    }
}