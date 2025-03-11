using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AINode : MonoBehaviour
{
    [SerializeField] List<Transform> connections;

    void Start()
    {
        General.GetObjectRoot(transform)?.GetComponent<AINodes>().AddNode(this);
    }

    [ShowInInspector]
    public List<Vector3> Directions => connections.ConvertAll(input => (input.transform.position - transform.position).normalized);
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