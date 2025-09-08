using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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

    void OnPowerChanged()
    {
        if (text != null){
            text.text = powerSystem.GlobalMinimalPower ? "Disable Backup Power" : "Enable Backup Power";
        }
    }

    public void Interact(Player player)
    {
        powerSystem.SetGlobalMinimalPower(!powerSystem.GlobalMinimalPower);
    }

    public float HoldDuration => 3;
}
