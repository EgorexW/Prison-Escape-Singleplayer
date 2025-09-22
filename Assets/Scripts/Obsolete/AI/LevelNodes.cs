using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelNodes : MonoBehaviour
{
    [SerializeField] List<LevelNode> corridorNodes;
    [SerializeField] List<LevelNode> roomNodes;

    public List<LevelNode> CorridorNodes => corridorNodes;
    public List<LevelNode> RoomNodes => roomNodes;
    public List<LevelNode> Nodes => new(CorridorNodes.Concat(RoomNodes));

    public void ResetNodes()
    {
        corridorNodes = new List<LevelNode>();
        roomNodes = new List<LevelNode>();
        var nodes = GetComponentsInChildren<LevelNode>();
        foreach (var node in nodes) AddNode(node);
    }

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
}