using TMPro;
using UnityEngine;

public class BackupPowerButton : MonoBehaviour, IInteractive
{
    [SerializeField] TextMeshPro text;

    MainPowerSystem powerSystem;

    void Start()
    {
        powerSystem = General.GetRootComponent<MainPowerSystem>(gameObject);
        powerSystem.OnPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged();
    }

    public void Interact(Player player)
    {
        powerSystem.SetGlobalMinimalPower(!powerSystem.GlobalMinimalPower);
    }

    public float HoldDuration => 3;

    void OnPowerChanged()
    {
        if (text != null){
            text.text = powerSystem.GlobalMinimalPower ? "Disable Backup Power" : "Enable Backup Power";
        }
    }
}