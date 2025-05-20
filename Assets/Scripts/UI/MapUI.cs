using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class MapUI : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] LevelNodes baseLevelNodes;
    [BoxGroup("References")][Required][SerializeField] MainAI mainAI;
    
    [FormerlySerializedAs("objectPool")] [BoxGroup("References")][Required][SerializeField] ObjectsUI roomsObjectPool;
    [BoxGroup("References")][Required][SerializeField] ObjectsUI aiObjectPool;
    [BoxGroup("References")][Required][SerializeField] GameObject container;
    
    [SerializeField] float scale = 0.1f;

    void Awake()
    {
        GenerateMap();
    }

    [Button]
    public void GenerateMap()
    {
        GenerateMap(baseLevelNodes, mainAI.GetActiveAIObjects());
    }
    public void GenerateMap(LevelNodes levelNodes, List<IAIObject> aiObjects)
    {
        var trueScale = scale * GetComponent<RectTransform>().sizeDelta.x;
        DrawRooms(levelNodes, trueScale);
        DrawAIObjects(aiObjects, trueScale);
    }

    void DrawAIObjects(List<IAIObject> aiObjects, float trueScale)
    {
        aiObjectPool.SetCount(aiObjects.Count);
        for (var i = 0; i < aiObjects.Count; i++)
        {
            var aiObject = aiObjects[i];
            var obj = aiObjectPool.GetActiveObjs()[i];
            var pos = new Vector3(aiObject.GameObject.transform.position.x, aiObject.GameObject.transform.position.z) * trueScale;
            obj.transform.localPosition = pos;
            obj.GetComponent<RectTransform>().sizeDelta = Vector2.one * trueScale;
        }
    }

    void DrawRooms(LevelNodes levelNodes, float trueScale)
    {
        roomsObjectPool.SetCount(levelNodes.Nodes.Count);
        var leftUpperCorner = Vector2.one;
        var rightLowerCorner = Vector2.one;
        for (var i = 0; i < levelNodes.Nodes.Count; i++)
        {
            var node = levelNodes.Nodes[i];
            var nodeBounds = node.Bounds;
            var nodePos = new Vector3(nodeBounds.center.x, nodeBounds.center.z) * trueScale;
            var nodeSize = new Vector3(nodeBounds.size.x, nodeBounds.size.z) * trueScale;
            var obj = roomsObjectPool.GetActiveObjs()[i];
            leftUpperCorner.x = Mathf.Min(leftUpperCorner.x, nodePos.x);
            leftUpperCorner.y = Mathf.Max(leftUpperCorner.y, nodePos.y);
            rightLowerCorner.x = Mathf.Max(rightLowerCorner.x, nodePos.x);
            rightLowerCorner.y = Mathf.Min(rightLowerCorner.y, nodePos.y);
            obj.transform.localPosition = nodePos;
            obj.GetComponent<RectTransform>().sizeDelta = nodeSize;
            obj.GetComponent<NodeUI>().SetNodeType(node.nodeType);
        }
        var center = (leftUpperCorner + rightLowerCorner) / 2;
        container.transform.localPosition = - new Vector3(center.x, center.y, 0);
    }
}