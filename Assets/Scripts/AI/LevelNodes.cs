using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectRoot))]
public class LevelNodes : MonoBehaviour
{
    [SerializeField] List<CorridorLevelNode> corridorNodes;
    
    public List<CorridorLevelNode> CorridorNodes => corridorNodes;

    public void AddNode(CorridorLevelNode node)
    {
        corridorNodes.Add(node);
    }

    public List<Vector3> GetCorridorNodesPos()
    {
        return corridorNodes.ConvertAll(input => input.transform.position);
    }
}