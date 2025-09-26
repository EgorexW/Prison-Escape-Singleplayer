using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] TextMeshPro text;

    void Update()
    {
        var timeLeft = GameManager.i.gameTime.TimeLeft;
        if (timeLeft <= 0){
            text.text = "00:00";
            return;
        }
        var minutes = Mathf.FloorToInt(timeLeft / 60f);
        var seconds = Mathf.FloorToInt(timeLeft % 60f);
        text.text = $"{minutes:00}:{seconds:00}";
    }
}