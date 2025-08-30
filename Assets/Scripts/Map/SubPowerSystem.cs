using System;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(BoxCollider))]
class SubPowerSystem : MonoBehaviour
{
    [FormerlySerializedAs("bounds")] [GetComponent][SerializeField] new BoxCollider collider;

    public PowerLevel power;
    
    void Awake()
    {
        collider.isTrigger = true;
    }

    public bool InBounds(Vector3 transformPosition)
    {
        return collider.bounds.Contains(transformPosition);
    }
}