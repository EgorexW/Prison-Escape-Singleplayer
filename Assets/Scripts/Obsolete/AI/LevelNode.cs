using System;
using System.Collections.Generic;
using System.Linq;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class LevelNode : MonoBehaviour
{
    const float DISTANCE = 7;
    
    [GetComponent][SerializeField] BoxCollider boxCollider;

    [FormerlySerializedAs("LayerMask")] [SerializeField] LayerMask layerMask;
    public NodeType nodeType = NodeType.Corridor;

    public Bounds Bounds => boxCollider.bounds;

    [HideInEditorMode][ShowInInspector] List<LevelNode> neighboringNodes = null;

    void Start()
    {
        General.GetObjectRoot(transform)?.GetComponent<LevelNodes>().AddNode(this);
    }

    public List<LevelNode> GetNeighboringNodes()
    {
        if (neighboringNodes != null){
            return neighboringNodes;
        }
        GenerateNeighbours();
        return neighboringNodes;
    }

    [Button]
    void GenerateNeighbours()
    {
        neighboringNodes = new();
        foreach (var direction in General.Get4MainDirections3D()){
            // Debug.Log("Checking direction: " + direction, this);
            var raycast = Physics.Raycast(transform.position, direction, out var raycastHit, DISTANCE,
                layerMask);
            // Debug.DrawRay(transform.position, direction * DISTANCE, Color.red, 2);
            if (!raycast){
                // Debug.Log("No collider hit", this);
                continue;
            }
            var levelNode = raycastHit.transform.GetComponent<LevelNode>();
            if (levelNode == null){
                // Debug.Log("No level node hit", this);
                continue;
            }
            neighboringNodes.Add(levelNode);
        }
    }

    public List<Vector3> SameTypeConnections => GetNeighboringNodes()
                .Where(input => input.nodeType == nodeType)
                .Select(input => input.transform.position - transform.position)
                .ToList();


    [HideInEditorMode][ShowInInspector]
    public List<Vector3> Directions => Connections.ConvertAll(input => input.normalized);

    [HideInEditorMode][ShowInInspector]
    public List<Vector3> Connections => GetNeighboringNodes().ConvertAll(input => input.transform.position - transform.position);
    
    [HideInEditorMode][ShowInInspector][ShowIf("@nodeType == NodeType.Corridor")]
    public CorridorNodeType CorridorNodeType{
        get{
            if (nodeType != NodeType.Corridor){
                // Debug.LogWarning("Tried to get corridor type of not corridor node", this);
                return default;
            }
            return SameTypeConnections.Count switch{
                1 => CorridorNodeType.DeadEnd,
                2 => Vector3.Dot(SameTypeConnections[0], SameTypeConnections[1]) < -0.5
                    ? CorridorNodeType.Straight
                    : CorridorNodeType.Turn,
                3 => CorridorNodeType.ThreeWay,
                4 => CorridorNodeType.FourWay,
                _ => default
            };
        }
    }
}

public enum NodeType
{
    Corridor,
    Room
}

[Flags]
public enum CorridorNodeType
{
    Straight = 1 << 0,
    Turn = 1 << 1,
    ThreeWay = 1 << 2,
    FourWay = 1 << 3,
    DeadEnd = 1 << 4
}