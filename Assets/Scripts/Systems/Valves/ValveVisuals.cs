using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class ValveVisuals : MonoBehaviour
{
    [BoxGroup("References")][GetComponent][SerializeField] Valve valve;
    [BoxGroup("References")][Required][SerializeField] Transform valveHandle;
    
    [SerializeField]    float rotateAngle = 360;
        [SerializeField]float rotateTime = 1;
    
    void Awake()
    {
        valve.onClick.AddListener(Rotate);
    }

    void Rotate(Player arg0)
    {
        valveHandle.LeanRotateAroundLocal(Vector3.forward, rotateAngle, rotateTime);
    }
}
