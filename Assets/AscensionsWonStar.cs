using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class AscensionsWonStar : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] RectMask2D mask;
    [SerializeField] int ascensionsCount = 7;

    void Awake()
    {
        float ascensionsWon = Ascensions.GetUnlockedAscension();
        var percent = Mathf.Clamp01(ascensionsWon / ascensionsCount);
        var topPadding = (1 - percent) * mask.rectTransform.sizeDelta.y;
        mask.padding = new Vector4(0, 0, 0, topPadding);
    }
}
