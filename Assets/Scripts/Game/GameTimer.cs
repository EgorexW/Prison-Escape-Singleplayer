using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] TextMeshPro text;

    void Update()
    {
        var timeLeft = GameDirector.i.gameTime.TimeLeft;
        if (timeLeft <= 0){
            text.text = "00:00";
            return;
        }
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        text.text = $"{minutes:00}:{seconds:00}";
    }
}
