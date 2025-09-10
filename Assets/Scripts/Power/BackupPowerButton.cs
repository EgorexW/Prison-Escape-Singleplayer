using TMPro;
using UnityEngine;

public class BackupPowerButton : MonoBehaviour, IInteractive
{
    [SerializeField] TextMeshPro text;

    void Start()
    {
        MainPowerSystem.i.OnPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged();
    }

    public void Interact(Player player)
    {
        MainPowerSystem.i.SetGlobalMinimalPower(!MainPowerSystem.i.GlobalMinimalPower);
    }

    public float HoldDuration => 3;

    void OnPowerChanged()
    {
        if (text != null){
            text.text = MainPowerSystem.i.GlobalMinimalPower ? "Disable Backup Power" : "Enable Backup Power";
        }
    }
}