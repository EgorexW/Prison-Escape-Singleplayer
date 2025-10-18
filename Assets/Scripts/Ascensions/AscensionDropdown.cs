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
        Debug.Log($"Unlocked Ascension {unlockedAscensions}", this);
        if (unlockedAscensions <= 0){
            Hide();
            return;
        }
        dropdown.options.Clear();
        for (int i = 0; i <= unlockedAscensions; i++){
            dropdown.options.Add(new TMP_Dropdown.OptionData($"Ascension {i}"));
        }
        dropdown.value = -1;
        // Debug.Log($"Ascension Level: {Ascensions.AscensionLevel}", this);
        dropdown.value = Ascensions.AscensionLevel;
        dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(int arg0)
    {
        Ascensions.SetAscensionLevel(arg0);
    }
}
