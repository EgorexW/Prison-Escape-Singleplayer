using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemVisuals : MonoBehaviour
{
    [FormerlySerializedAs("keycardName")] public string displayName;
    [FormerlySerializedAs("keycardColor")] public Color color = Color.white;

    [BoxGroup("References")] [SerializeField] List<TextMeshPro> keycardTexts;
    [BoxGroup("References")] [SerializeField] List<Image> keycardImages;

    void Reset()
    {
        keycardTexts = new List<TextMeshPro>(GetComponentsInChildren<TextMeshPro>(true));
        keycardImages = new List<Image>(GetComponentsInChildren<Image>(true));
    }

    protected void Start()
    {
        Apply();
    }

    [Button]
    void Apply()
    {
        foreach (var text in keycardTexts) text.text = displayName;
        // text.color = keycardColor;
        foreach (var image in keycardImages) image.color = color;
    }
}