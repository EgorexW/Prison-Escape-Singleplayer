using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectRoot))]
public class LevelNodes : MonoBehaviour
{
    [SerializeField] List<LevelNode> corridorNodes;
    
    public List<LevelNode> CorridorNodes => corridorNodes;

    public void AddNode(LevelNode node)
    {
        corridorNodes.Add(node);
    }

    public List<Vector3> GetCorridorNodesPos()
    {
        return corridorNodes.ConvertAll(input => input.transform.position);
    }
}