using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class NodeUI : SerializedMonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Image image;
    [BoxGroup("References")] [Required] [SerializeField] TextMeshProUGUI text;

    [SerializeField] Dictionary<NodeType, Sprite> nodeSprites = new();
    [SerializeField] Sprite discoveredSprite;

    public void SetNode(LevelNode node)
    {
        image.sprite = nodeSprites[node.type];
        text.text = "";
        if (node.type == NodeType.Corridor){
            return;
        }
        var roomNode = node as RoomNode;
        Debug.Assert(roomNode != null, nameof(roomNode) + " != null");
        if (!roomNode.room.discovered){
            return;
        }
        image.sprite = discoveredSprite;
        text.text = roomNode.room.Name;
        if (roomNode.room.doorway != null && roomNode.room.doorway.accessLevel != null){
            image.color = roomNode.room.doorway.accessLevel.color;
            return;
        }
        var bgColor = image.color;
        var brightness = bgColor.r * 0.299f + bgColor.g * 0.587f + bgColor.b * 0.114f;
        text.color = brightness > 0.5f ? Color.black : Color.white;
    }
}