using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class AscensionDropdown : UIElement
{
    [BoxGroup("References")] [Required] [SerializeField] TMP_Dropdown dropdown;

    public override void Show()
    {
        base.Show();
        var unlockedAscensions = Ascensions.GetUnlockedAscension();
        // Debug.LogError($"Unlocked Ascension {unlockedAscensions}", this);
        if (unlockedAscensions <= 0){
            Hide();
            return;
        }
        dropdown.options.Clear();
        for (var i = 0; i <= unlockedAscensions; i++) dropdown.options.Add(new TMP_Dropdown.OptionData($"Level {i}"));
        dropdown.value = -1;
        // Debug.Log($"Ascension Level: {Ascensions.AscensionLevel}", this);
        dropdown.onValueChanged.AddListener(OnValueChanged);
        dropdown.value = Ascensions.AscensionLevel;
    }

    void OnValueChanged(int arg0)
    {
        Ascensions.SetAscensionLevel(arg0);
    }
}