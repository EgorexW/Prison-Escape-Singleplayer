using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class SubPowerSystem : MonoBehaviour
{
    [FormerlySerializedAs("bounds")] [GetComponent] [SerializeField] new BoxCollider collider;
    public Bounds Bounds => collider.bounds;

    public PowerLevel power;

    void Awake()
    {
        collider.isTrigger = true;
    }

    public bool InBounds(Vector3 transformPosition)
    {
        return Bounds.Contains(transformPosition);
    }
}