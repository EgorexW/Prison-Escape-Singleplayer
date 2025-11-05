using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    const float AI_OBJECT_SIZE = 3;

    [FormerlySerializedAs("rect")] [GetComponent] [SerializeField] RectTransform rectTransform;

    [BoxGroup("Internal References")] [Required] [SerializeField] ObjectsUI roomsObjectPool;
    [BoxGroup("Internal References")] [Required] [SerializeField] GameObject container;
    [BoxGroup("Internal References")] [SerializeField] RectTransform playerPointer;
    [BoxGroup("Internal References")] [SerializeField] RectTransform selfPointer;
    [BoxGroup("Internal References")] [SerializeField] ObjectsUI powerSystemsPool;

    [SerializeField] float scale = 0.005f;

    float Rect => Mathf.Min(rectTransform.rect.width, rectTransform.rect.height);
    float TrueScale => scale * Rect;

    void Start()
    {
        MainPowerSystem.i.OnPowerChanged.AddListener(DrawPowerZones);
    }

    void Update()
    {
        DrawPlayer();
    }

    void OnEnable()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        GenerateMap(GameManager.i.levelNodes);
    }

    public void GenerateMap(LevelNodes levelNodes)
    {
        DrawRooms(levelNodes);
        DrawSelf();
        DrawPowerZones();
    }

    void DrawPowerZones()
    {
        if (powerSystemsPool == null){
            return;
        }
        var subPowerSystems = MainPowerSystem.i.SubPowerSystems;
        powerSystemsPool.SetCount(subPowerSystems.Count);
        for (int i = 0; i < subPowerSystems.Count; i++){
            var rect = powerSystemsPool.GetActiveObjs()[i].GetComponent<RectTransform>();
            var subPowerSystem = subPowerSystems[i];
            rect.sizeDelta = new Vector2(subPowerSystem.Bounds.size.x, subPowerSystem.Bounds.size.z) * TrueScale;
            var center = subPowerSystem.Bounds.center;
            rect.localPosition = new Vector2(center.x, center.z) * TrueScale;
            Color color = subPowerSystem.power switch
            {
                PowerLevel.NoPower => Color.red,
                PowerLevel.MinimalPower => Color.yellow,
                PowerLevel.FullPower => Color.green,
                _ => Color.gray
            };
            powerSystemsPool.GetActiveObjs()[i].GetComponent<Image>().color = color;
        }
    }

    void DrawSelf()
    {
        if (selfPointer != null){
            var position = transform.position;
            PlacePointer(position, selfPointer);
        }
    }

    void DrawPlayer()
    {
        if (playerPointer != null){
            var position = GameManager.i.Player.transform.position;
            PlacePointer(position, playerPointer);
        }
    }

    void PlacePointer(Vector3 position, RectTransform pointer)
    {
        var playerPos = new Vector2(position.x, position.z);
        pointer.localPosition = playerPos * TrueScale;
        pointer.sizeDelta = Vector2.one * AI_OBJECT_SIZE * TrueScale;
    }

    void DrawRooms(LevelNodes levelNodes)
    {
        roomsObjectPool.SetCount(levelNodes.Nodes.Count);
        var min = new Vector2(float.MaxValue, float.MaxValue);
        var max = new Vector2(float.MinValue, float.MinValue);
        for (var i = 0; i < levelNodes.Nodes.Count; i++){
            var node = levelNodes.Nodes[i];
            var nodeBounds = node.Bounds;
            var nodePos = new Vector2(nodeBounds.center.x, nodeBounds.center.z) * TrueScale;
            var nodeSize = new Vector2(nodeBounds.size.x, nodeBounds.size.z) * TrueScale;
            var obj = roomsObjectPool.GetActiveObjs()[i];

            min = Vector2.Min(min, nodePos - nodeSize / 2);
            max = Vector2.Max(max, nodePos + nodeSize / 2);

            obj.transform.localPosition = nodePos;
            obj.GetComponent<RectTransform>().sizeDelta = nodeSize;
            var nodeUI = obj.GetComponent<NodeUI>();
            nodeUI.SetNode(node);
        }
        var center = (min + max) / 2;
        container.GetComponent<RectTransform>().anchoredPosition = -center;
    }
}