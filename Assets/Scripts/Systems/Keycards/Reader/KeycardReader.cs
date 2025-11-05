using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class KeycardReader : PoweredDevice, IInteractive
{
    [BoxGroup("References")] [Required] [SerializeField] public AccessLevel accessLevel;

    [FormerlySerializedAs("effects")] [SerializeField] public KeycardReaderVisuals visuals;
    [SerializeField] List<KeycardReader> linkedReaders;

    public bool stealCard;

    [FoldoutGroup("Electrocution")] [SerializeField] public Damage electrocutionDamage;
    [FoldoutGroup("Electrocution")] [SerializeField] public float baseElectrocutionChance;
    [FoldoutGroup("Electrocution")] [SerializeField] float minimalPowerElectrocutionChance = 0.5f;

    [FoldoutGroup("Events")] public UnityEvent onUnlock;
    
    bool corrupted;

    void Awake()
    {
        if (visuals != null){
            visuals.keycardReader = this;
        }
    }

    public void Interact(Player player)
    {
        if (corrupted){
            visuals?.Corrupted();
            TryElectrocute(player);
            return;
        }
        if (!IsPowered()){
            return;
        }
        var item = player.GetHeldItem();
        if (item == null){
            visuals?.AccessDenied();
            return;
        }
        var keycard = item.GetComponent<Keycard>();
        if (keycard == null){
            visuals?.AccessDenied();
            return;
        }
        if (keycard.hackChance){
            if (!(Random.value < keycard.hackChance)){
                visuals?.Corrupted();
                corrupted = true;
                return;
            }
        }
        else if (!keycard.ReadKeycard(accessLevel)){
            visuals?.AccessDenied();
            return;
        }
        if (keycard.oneUse || stealCard){
            player.RemoveItem(item);
            Destroy(item.gameObject);
        }
        AccessGranted(true);
        TryElectrocute(player);
    }

    public float HoldDuration => 1;

    public void AccessGranted(bool original)
    {
        visuals?.AccessGranted(original);
        onUnlock.Invoke();
        BroadcastMessage("Unlock", SendMessageOptions.DontRequireReceiver);
        if (!original){
            return;
        }
        foreach (var reader in linkedReaders) reader.AccessGranted(false);
    }

    void TryElectrocute(Player player)
    {
        var electrocutionChance = GetPowerLevel() == PowerLevel.MinimalPower ? minimalPowerElectrocutionChance : 0;
        electrocutionChance = baseElectrocutionChance + electrocutionChance;
        if (Random.value < electrocutionChance){
            player.playerHealth.Damage(electrocutionDamage);
            visuals?.Electrocute();
        }
    }
}