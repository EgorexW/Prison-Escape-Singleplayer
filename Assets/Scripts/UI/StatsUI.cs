using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class StatsUI : UIElement
{
    [BoxGroup("References")] [Required] [SerializeField] TextMeshProUGUI text;

    public override void Show()
    {
        base.Show();
        var stats = GameStats.i;
        if (stats == null){
            text.text = "";
            Debug.LogWarning("No GameStats instance found.", this);
            return;
        }
        text.text = stats.GetStatsDescription();
    }
}