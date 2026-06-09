using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public static class LevelNodeExtension
{
    const float DISTANCE = 7;
    static LayerMask LevelNodeLayerMask => LayerMask.GetMask("Level Node");

    public static List<Vector3> SameTypeConnections(this LevelNode node)
    {
        return node.GetNeighboringNodes()
            .Where(input => input.type == node.type)
            .Select(input => input.transform.position - node.transform.position)
            .ToList();
    }


    [HideInEditorMode]
    [ShowInInspector]
    public static List<Vector3> Directions(this LevelNode node)
    {
        return node.Connections().ConvertAll(input => input.normalized);
    }

    [HideInEditorMode]
    [ShowInInspector]
    public static List<Vector3> Connections(this LevelNode node)
    {
        return node.GetNeighboringNodes().ConvertAll(input => input.transform.position - node.transform.position);
    }

    public static List<LevelNode> GetNeighboringNodes(this LevelNode node)
    {
        var neighboringNodes = new List<LevelNode>();
        foreach (var direction in General.Get4MainDirections3D()){
            // Debug.Log("Checking direction: " + direction, this);
            var raycast = Physics.Raycast(node.transform.position, direction, out var raycastHit, DISTANCE,
                LevelNodeLayerMask);
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
        return neighboringNodes;
    }

    public static LevelNode GetNode(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.1f, LevelNodeLayerMask);
    
        if (hitColliders.Length > 0)
        {
            if (hitColliders.Length > 1){
                Debug.LogWarning($"Multiple colliders hit at position {position}, returning the first one.", hitColliders[0]);
            }
            return hitColliders[0].GetComponent<LevelNode>();
        }
    
        return null;
    }
}