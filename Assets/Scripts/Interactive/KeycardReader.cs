using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class KeycardReader : PoweredDevice, IInteractive
{
    [BoxGroup("References")] [Required] [SerializeField] public AccessLevel accessLevel;

    [FormerlySerializedAs("effects")] [SerializeField] KeycardReaderVisuals visuals;

    [FoldoutGroup("Electrocution")] [SerializeField] public Damage electrocutionDamage;
    [FoldoutGroup("Electrocution")] [SerializeField] public float baseElectrocutionChance;
    [FoldoutGroup("Electrocution")] [SerializeField] float minimalPowerElectrocutionChance = 0.5f;

    [FoldoutGroup("Events")] public UnityEvent onUnlock;

    void Awake()
    {
        if (visuals != null){
            visuals.keycardReader = this;
        }
    }

    public void Interact(Player player)
    {
        if (!IsPowered()){
            return;
        }
        var item = player.GetHeldItem();
        if (item == null){
            visuals?.AccessDenied();
            return;
        }
        var keycard = item.GetComponent<IKeycard>();
        if (keycard == null || !keycard.HasAccess(accessLevel)){
            visuals?.AccessDenied();
            return;
        }
        Unlock();
        TryElectrocute(player);
    }

    void Unlock()
    {
        visuals?.AccessGranted();
        onUnlock.Invoke();
        BroadcastMessage("Unlock", SendMessageOptions.DontRequireReceiver);
    }

    public float HoldDuration => 1;

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