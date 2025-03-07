using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetricBar : UIElement
{
    [SerializeField][Required] Slider slider;
    [SerializeField] TextMeshProUGUI text;
    
    public void Set(float value, float maxValue = -1){
        Show();
        if (maxValue > 0){
            slider.maxValue = maxValue;
        }
        slider.value = value;
        if (text != null){
            text.text = Mathf.Round(value).ToString();
        }
    }
}