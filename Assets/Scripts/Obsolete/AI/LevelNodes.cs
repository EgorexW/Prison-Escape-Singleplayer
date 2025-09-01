using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ObjectRoot))]
public class LevelNodes : MonoBehaviour
{
    [SerializeField] List<LevelNode> corridorNodes;
    [SerializeField] List<LevelNode> roomNodes;
    
    public List<LevelNode> CorridorNodes => corridorNodes;
    public List<LevelNode> RoomNodes => roomNodes;
    public List<LevelNode> Nodes => new (CorridorNodes.Concat(RoomNodes));

    public void AddNode(LevelNode node)
    {
        if (node == null){
            Debug.LogWarning("Node is null", this);
            return;
        }
        switch (node.nodeType){
            case NodeType.Corridor:
                corridorNodes.Add(node);
                break;
            case NodeType.Room:
                roomNodes.Add(node);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public List<Vector3> GetCorridorNodesPos()
    {
        return corridorNodes.ConvertAll(input => input.transform.position);
    }
}