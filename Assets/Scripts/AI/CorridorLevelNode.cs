using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class CorridorLevelNode : MonoBehaviour
{
    const float COLLIDER_CHECK_SIZE = 0.1f;

    [FormerlySerializedAs("LayerMask")] [SerializeField] LayerMask layerMask;
    [SerializeField] List<Transform> connections;

    [ShowInInspector][ReadOnly] List<Vector3> corridorConnections = null;
    void Start()
    {
        General.GetObjectRoot(transform)?.GetComponent<LevelNodes>().AddNode(this);
    }

    public List<Vector3> GetCorridorConnections()
    {
        if (corridorConnections != null){
            return corridorConnections;
        }
        corridorConnections = new();
        foreach (var connection in Connections){
            var raycast = Physics.Raycast(transform.position, connection.normalized, out var raycastHit, connection.magnitude * 2,
                layerMask);
            if (!raycast) continue;
            var levelNode = raycastHit.transform.GetComponent<CorridorLevelNode>();
            if (levelNode != null){
                corridorConnections.Add(connection);
            }
        }
        return corridorConnections;
    }


    [ShowInInspector]
    public List<Vector3> Directions => Connections.ConvertAll(input => input.normalized);

    [ShowInInspector]
    public List<Vector3> Connections => connections.ConvertAll(input => input.transform.position - transform.position);
    
    [ShowInInspector] 
    public AINodeType NodeType{
        get{
            return connections.Count switch{
                2 => Vector3.Dot(connections[0].localPosition.normalized, connections[1].localPosition.normalized) < -0.5
                    ? AINodeType.CorridorStraight
                    : AINodeType.CorridorTurn,
                3 => AINodeType.ThreeWay,
                4 => AINodeType.FourWay,
                _ => throw new ArgumentOutOfRangeException("connections", "Connections must be 2, 3, 4")
            };
        }
    }
}

[Flags]
public enum AINodeType
{
    CorridorStraight = 1 << 0,
    CorridorTurn = 1 << 1,
    ThreeWay = 1 << 2,
    FourWay = 1 << 3
}