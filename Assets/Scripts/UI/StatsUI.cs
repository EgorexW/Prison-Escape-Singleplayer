using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class StatsUI : UIElement
{
    [BoxGroup("References")][Required][SerializeField] TextMeshProUGUI text;
    
    public override void Show()
    {
        base.Show();
        var stats = GameStats.i;
        if (stats == null){
            text.text = "No Stats Available";
            Debug.LogWarning("No GameStats instance found.", this);
            return;
        }
        var lines = new List<string>();
        lines.Add($"Game Time: {TimeSpan.FromSeconds(stats.gameTime):hh\\:mm\\:ss}");
        lines.Add($"Normal Damage Taken: {Mathf.Round(stats.normalDamageTaken)}");
        lines.Add($"Pernament Damage Taken: {Mathf.Round(stats.pernamentDamageTaken)}");
        lines.Add($"Meters Walked: {Mathf.Round(stats.metersWalked)}");
        text.text = string.Join("\n", lines);
    }
}
