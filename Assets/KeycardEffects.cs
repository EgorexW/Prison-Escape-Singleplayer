using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeycardEffects : MonoBehaviour
{
    [SerializeField] string keycardName;
    [SerializeField] Color keycardColor = Color.white;
    
    [BoxGroup("References")][SerializeField] List<TextMeshPro> keycardTexts;
    [BoxGroup("References")][SerializeField] List<Image> keycardImages;
    
    void Start()
    {
        Apply();
    }

    [Button]
    void Apply()
    {
        foreach (var text in keycardTexts){
            text.text = keycardName;
            // text.color = keycardColor;
        }
        foreach (var image in keycardImages){
            image.color = keycardColor;
        }
    }
    
    void Reset()
    {
        keycardTexts = new List<TextMeshPro>(GetComponentsInChildren<TextMeshPro>(includeInactive: true));
        keycardImages = new List<Image>(GetComponentsInChildren<Image>(includeInactive: true));
    }
}
