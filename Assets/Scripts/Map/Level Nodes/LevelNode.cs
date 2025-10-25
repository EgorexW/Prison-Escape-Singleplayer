using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class LevelNode : MonoBehaviour
{
    [GetComponent] [SerializeField] BoxCollider boxCollider;
    public abstract NodeType type{ get; }
    public Bounds Bounds => boxCollider.bounds;
}

public enum NodeType
{
    Corridor,
    Room
}