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
        dropdown.onValueChanged.AddListener(OnValueChanged);
        dropdown.value = Ascensions.AscensionLevel;
    }

    void OnValueChanged(int arg0)
    {
        Ascensions.AscensionLevel = arg0;
    }
}
