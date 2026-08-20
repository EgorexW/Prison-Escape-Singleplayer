using TMPro;
using UnityEngine;

public class BackupPowerInteractButton : InteractButton
{
    [SerializeField] TextMeshPro text;

    void Start()
    {
        MainPowerSystem.i.OnPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged();
    }

    public override void OnClick(Player player)
    {
        base.OnClick(player);
        MainPowerSystem.i.SetGlobalMinimalPower(!MainPowerSystem.i.GlobalMinimalPower);
    }

    void OnPowerChanged()
    {
        if (text != null){
            text.text = MainPowerSystem.i.GlobalMinimalPower ? "Disable Backup Power" : "Enable Backup Power";
        }
    }
}