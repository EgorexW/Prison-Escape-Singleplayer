using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class KeycardReader : PoweredDevice, IInteractive
{
    [BoxGroup("References")] [Required] [SerializeField] public AccessLevel accessLevel;
    [BoxGroup("References")] [Required] [SerializeField] public DoorLock doorLock;

    [FormerlySerializedAs("effects")] [SerializeField] KeycardReaderVisuals visuals;

    [BoxGroup("Electrocution")] [SerializeField] public Damage electrocutionDamage;
    [BoxGroup("Electrocution")] [SerializeField] public float baseElectrocutionChance;
    [BoxGroup("Electrocution")] [SerializeField] float minimalPowerElectrocutionChance = 0.5f;

    void Awake()
    {
        doorLock.unlocked = false;
        if (visuals != null){
            visuals.keycardReader = this;
        }
    }

    public void Interact(Player player)
    {
        if (GetPowerLevel() == PowerLevel.NoPower){
            return;
        }
        var item = player.GetHeldItem();
        var keycard = item.GetComponent<IKeycard>();
        if (keycard == null || !keycard.HasAccess(accessLevel)){
            visuals?.AccessDenied();
            return;
        }
        visuals?.AccessGranted();
        doorLock.unlocked = true;
        TryElectrocute(player);
    }

    public float HoldDuration => 1;

    void TryElectrocute(Player player)
    {
        var electrocutionChance = GetPowerLevel() == PowerLevel.MinimalPower ? minimalPowerElectrocutionChance : 0;
        electrocutionChance = baseElectrocutionChance + electrocutionChance;
        if (Random.value < electrocutionChance){
            player.Damage(electrocutionDamage);
            visuals?.Electrocute();
        }
    }
}