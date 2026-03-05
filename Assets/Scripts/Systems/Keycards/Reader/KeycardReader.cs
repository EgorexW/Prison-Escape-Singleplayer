using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class KeycardReader : PoweredDevice, IInteractive, IDamagable
{
    [BoxGroup("References")] [Required] [SerializeField] AccessLevel accessLevel;

    [FormerlySerializedAs("effects")] [SerializeField] KeycardReaderVisuals visuals;
    [SerializeField] List<KeycardReader> linkedReaders;

    [SerializeField] [FormerlySerializedAs("stealCard")] bool stealKeycard;
    
    [SerializeField][FoldoutGroup("Health")] Health health = new Health(1){
        damagedBy = DamageType.Electric
    };
    [SerializeField] float chanceToUnlockOnDestroy = 0.1f;
    [SerializeField] float hackResistance = 1;

    [FoldoutGroup("Electrocution")] [SerializeField] public Damage electrocutionDamage;
    [FoldoutGroup("Electrocution")] [SerializeField] public float baseElectrocutionChance;
    [FoldoutGroup("Electrocution")] [SerializeField] float minimalPowerElectrocutionChance = 0.5f;

    [FoldoutGroup("Events")] public UnityEvent onUnlock;

    bool corrupted;

    public bool Corrupted{
        get => corrupted;
        set{
            corrupted = value;
            visuals?.UpdateVisual();
        }
    }
    public AccessLevel AccessLevel{
        set{
            accessLevel = value;
            visuals?.UpdateVisual();
        }
        get => accessLevel;
    }

    public bool StealKeycard{
        get => stealKeycard;
        set{
            stealKeycard = value;
            visuals?.UpdateVisual();
        }
    }

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
        if (!keycard.ReadKeycard(accessLevel)){
            if (keycard.hackStrenght){
                if (Random.value > keycard.hackStrenght/hackResistance){
                    visuals?.Corrupted();
                    corrupted = true;
                    return;
                }
            }
            else{
                visuals?.AccessDenied();
                return;
            }
        }
        if (keycard.oneUse || stealKeycard){
            player.RemoveItem(item);
            Destroy(item.gameObject);
        }
        TryElectrocute(player);
        AccessGranted(true);
    }

    public float HoldDuration => 0.5f;

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

    public Health Health => health;
    public void Damage(Damage damage)
    {
        health.Damage(damage);
        visuals?.Electrocute();
        if (health.Alive){
            return;
        }
        OnDie();
    }

    void OnDie()
    {
        if (Random.value > chanceToUnlockOnDestroy/hackResistance){
            visuals?.Corrupted();
            corrupted = true;
            return;
        }
        AccessGranted(true);
    }
}