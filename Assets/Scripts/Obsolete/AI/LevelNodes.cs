using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class LevelNodes : MonoBehaviour
{
    [SerializeField] List<LevelNode> corridorNodes;
    [SerializeField] List<LevelNode> roomNodes;
    
    public static LevelNodes i;

    public List<LevelNode> CorridorNodes => corridorNodes;
    public List<LevelNode> RoomNodes => roomNodes;
    public List<LevelNode> Nodes => new(CorridorNodes.Concat(RoomNodes));

    void Awake()
    {
        if (i != null && i != this){
            Debug.LogWarning("Multiple LevelNodes in scene", this);
            Destroy(this);
            return;
        }
        i = this;
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

    public List<Vector3> GetCorridorNodesPos()
    {
        return corridorNodes.ConvertAll(input => input.transform.position);
    }
}