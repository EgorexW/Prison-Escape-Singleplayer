using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class GameModifiersDisplay: MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] TextMeshPro text;

    [SerializeField] GameObject objToRemove;

    string displayText;

    void Start()
    {
        if (Ascensions.AscensionLevel == 0){
            Destroy(gameObject);
            return;
        }
        Destroy(objToRemove);
        var effects = GameManager.i.gameModifiers.GetActiveModifiers();
        var texts = new string[effects.Count];
        for (var i = 0; i < effects.Count; i++) texts[i] = $"Level {i + 1} " + effects[i].GetEffectDescription();
        displayText = string.Join("\n", texts);
        text.text = displayText;
    }
}