using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : SerializedMonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Image image;
    [BoxGroup("References")] [Required] [SerializeField] TextMeshProUGUI text;

    [SerializeField] Dictionary<NodeType, Sprite> nodeSprites = new();
    [SerializeField] Sprite discoveredSprite;

    public void SetNode(LevelNode node)
    {
        if (node.room == null || !node.room.discovered){
            image.sprite = nodeSprites[node.nodeType];
            text.text = "";
            return;
        }
        image.sprite = discoveredSprite;
        text.text = node.room.Name;
        if (node.room.doorway != null && node.room.doorway.accessLevel != null){
            image.color = node.room.doorway.accessLevel.color;
            return;
        }
        var bgColor = image.color;
        var brightness = bgColor.r * 0.299f + bgColor.g * 0.587f + bgColor.b * 0.114f;
        text.color = brightness > 0.5f ? Color.black : Color.white;
    }
}