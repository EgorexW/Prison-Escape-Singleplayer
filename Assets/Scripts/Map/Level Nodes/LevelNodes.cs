using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelNodes : MonoBehaviour
{
    [SerializeField] List<CorridorNode> corridorNodes;
    [SerializeField] List<RoomNode> roomNodes;

    public List<CorridorNode> CorridorNodes => corridorNodes;
    public List<RoomNode> RoomNodes => roomNodes;
    public List<LevelNode> Nodes => new(CorridorNodes.Concat<LevelNode>(RoomNodes));

    public void ResetNodes()
    {
        corridorNodes = new List<CorridorNode>();
        roomNodes = new List<RoomNode>();
        var nodes = GetComponentsInChildren<LevelNode>();
        foreach (var node in nodes) AddNode(node);
    }

    public void AddNode(LevelNode node)
    {
        if (node == null){
            Debug.LogWarning("Node is null", this);
            return;
        }
        switch (node.type){
            case NodeType.Corridor:
                corridorNodes.Add(node as CorridorNode);
                break;
            case NodeType.Room:
                roomNodes.Add(node as RoomNode);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}