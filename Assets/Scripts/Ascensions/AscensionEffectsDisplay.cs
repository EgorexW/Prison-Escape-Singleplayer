using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class AscensionEffectsDisplay : PoweredDevice
{
    [BoxGroup("References")][Required][SerializeField] TextMeshPro text;

    [SerializeField] GameObject objToRemove;
    
    string displayText;

    protected override void Start()
    {
        base.Start();
        if (Ascensions.AscensionLevel == 0){
            Destroy(gameObject);
            return;
        }
        Destroy(objToRemove);
        var effects = GameManager.i.ascensions.GetActiveEffects();
        var texts = new string[effects.Count];
        for (int i = 0; i < effects.Count; i++){
            texts[i] = $"Level {i+1}" + effects[i].GetEffectDescription();
        }
        displayText = string.Join("\n", texts);
        text.text = displayText;
        OnPowerChanged();
    }

    protected override void OnPowerChanged()
    {
        base.OnPowerChanged();
        text.gameObject.SetActive(IsPowered());
    }
}
