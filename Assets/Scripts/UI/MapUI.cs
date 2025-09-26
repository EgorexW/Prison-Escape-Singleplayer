using Sirenix.OdinInspector;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    const float AI_OBJECT_SIZE = 3;
    [BoxGroup("Internal References")] [Required] [SerializeField] ObjectsUI roomsObjectPool;
    [BoxGroup("Internal References")] [Required] [SerializeField] GameObject container;
    [BoxGroup("Internal References")] [SerializeField] RectTransform playerPointer;
    [BoxGroup("Internal References")] [SerializeField] RectTransform selfPointer;

    [SerializeField] float scale = 0.005f;

    void Awake()
    {
        General.CallAfterSeconds(GenerateMap, 5f);
    }

    [Button]
    public void GenerateMap()
    {
        GenerateMap(GameManager.i.levelNodes);
    }

    public void GenerateMap(LevelNodes levelNodes)
    {
        var rect = Mathf.Min(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
        var trueScale = scale * rect;
        DrawRooms(levelNodes, trueScale);
        DrawPlayer(trueScale);
        DrawSelf(trueScale);
    }

    void DrawSelf(float trueScale)
    {
        if (selfPointer != null){
            var position = transform.position;
            PlacePointer(trueScale, position, selfPointer);
        }
    }

    void DrawPlayer(float trueScale)
    {
        if (playerPointer != null){
            var position = GameManager.i.Player.transform.position;
            PlacePointer(trueScale, position, playerPointer);
        }
    }

    void PlacePointer(float trueScale, Vector3 position, RectTransform pointer)
    {
        var playerPos = new Vector2(position.x, position.z);
        pointer.localPosition = playerPos * trueScale;
        pointer.sizeDelta = Vector2.one * AI_OBJECT_SIZE * trueScale;
    }

    void DrawRooms(LevelNodes levelNodes, float trueScale)
    {
        roomsObjectPool.SetCount(levelNodes.Nodes.Count);
        var min = new Vector2(float.MaxValue, float.MaxValue);
        var max = new Vector2(float.MinValue, float.MinValue);
        for (var i = 0; i < levelNodes.Nodes.Count; i++){
            var node = levelNodes.Nodes[i];
            var nodeBounds = node.Bounds;
            var nodePos = new Vector2(nodeBounds.center.x, nodeBounds.center.z) * trueScale;
            var nodeSize = new Vector2(nodeBounds.size.x, nodeBounds.size.z) * trueScale;
            var obj = roomsObjectPool.GetActiveObjs()[i];

            min = Vector2.Min(min, nodePos - nodeSize / 2);
            max = Vector2.Max(max, nodePos + nodeSize / 2);

            obj.transform.localPosition = nodePos;
            obj.GetComponent<RectTransform>().sizeDelta = nodeSize;
            obj.GetComponent<NodeUI>().SetNodeType(node.nodeType);
        }
        var center = (min + max) / 2;
        container.GetComponent<RectTransform>().anchoredPosition = -center;
    }
}