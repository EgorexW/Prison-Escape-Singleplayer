using Sirenix.OdinInspector;
using UnityEngine;

class TargetsSeeing : MonoBehaviour
{
    [SerializeField] bool log;

    [SerializeField] [Range(0, 90)] float maxAngle = 45;
    [SerializeField] float maxDis = 15;

    [Required] [SerializeField] Transform seePoint;
    [SerializeField] LayerMask layerMask;

    public float CheckTargetVisibility(GameObject target)
    {
        var direction = target.transform.position - transform.position;
        var distance = direction.magnitude;
        if (distance > maxDis){
            if (log){
                Debug.Log("Target too far away");
            }
            return 0;
        }
        var angle = Vector3.Angle(direction, transform.forward);
        if (angle > maxAngle){
            if (log){
                Debug.Log("Target too far to the side, angle: " + angle);
            }
            // TODO Seems to detect mostly to the right side
            return 0;
        }
        var ray = new Ray(transform.position, direction);
        if (log){
            Debug.DrawRay(ray.origin, ray.direction * maxDis, Color.red, 0.1f);
        }
        if (!Physics.Raycast(ray, out var hit, maxDis, layerMask)){
            if (log){
                Debug.Log("Raycast failed");
            }
            return 0;
        }
        var hitGameObject = hit.transform.gameObject;
        if (hitGameObject != target){
            if (log){
                Debug.Log("Hit Object is not the target: " + hitGameObject.name, hitGameObject);
                Debug.Log($"Target is {target.name}", target);
            }
            return 0;
        }
        var visibility = (maxDis - distance) / maxDis;
        if (log){
            Debug.Log($"Target Visibility: {visibility}");
        }
        return visibility;
    }
}