using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class WeightUI : MonoBehaviour
{
    [Required] [SerializeField] Image weightIcon;
    
    [SerializeField] List<WeightIconUI> weightIcons;
    
    public void ShowWeight(float weight)
    {
        WeightIconUI icon = null;
        foreach (var iconTmp in weightIcons){
            if (!(weight >= iconTmp.threshold)){
                continue;
            }
            if (icon == null || iconTmp.threshold > icon.threshold){
                icon = iconTmp;
            }
        }
        if (icon == null){
            weightIcon.gameObject.SetActive(false);
            return;
        }
        weightIcon.gameObject.SetActive(true);
        weightIcon.sprite = icon.sprite;
        weightIcon.color = icon.color;
    }
}

[Serializable]
class WeightIconUI
{
    public float threshold;
    public Sprite sprite;
    public Color color;
}