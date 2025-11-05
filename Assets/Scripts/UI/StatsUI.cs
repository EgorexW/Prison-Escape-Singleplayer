using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class StatsUI : UIElement
{
    [BoxGroup("References")] [Required] [SerializeField] TextMeshProUGUI text;

    public override void Show()
    {
        base.Show();
        var stats = GameStats.i.GetStats();
        if (stats == null){
            text.text = "";
            Debug.LogWarning("No Stats got", this);
            return;
        }
        text.text = Descriptions.GetStatsDescription(stats);
    }
}