using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : SerializedMonoBehaviour
{
    [GetComponent] [SerializeField] Image image;

    [SerializeField] Dictionary<NodeType, Sprite> nodeSprites = new();

    public void SetNodeType(NodeType nodeType)
    {
        image.sprite = nodeSprites[nodeType];
    }
}