using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectRoot))]
public class AINodes : MonoBehaviour
{
    [SerializeField] List<AINode> corridorNodes;
    
    public List<AINode> CorridorNodes => corridorNodes;

    public void AddNode(AINode node)
    {
        corridorNodes.Add(node);
    }

    public List<Vector3> GetCorridorNodesPos()
    {
        return corridorNodes.ConvertAll(input => input.transform.position);
    }
}