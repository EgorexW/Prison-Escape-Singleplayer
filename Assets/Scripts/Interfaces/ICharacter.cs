using UnityEngine;
using UnityEngine.Events;

public interface ICharacter : IDamagable{
    // GetTransforms
    public Transform GetTransform();
    public Transform GetAimTransform();
    void SetPos(Vector3 position);

    public IInventory GetInventory();

    //Item Actions
    void PickupItem(IItem item);
    void DropItem(IItem item = null);
    void RemoveItem(IItem item);
    IItem GetHeldItem();
    void EquipItem(IItem item);
    void UseHeldItem(bool alternative = false);
    void HoldUseHeldItem(bool alternative = false);
    void StopUseHeldItem(bool alternative = false);

    // StatusEffects
    void AddStatusEffect(IStatusEffect statusEffect);
    void RemoveStatusEffect(IStatusEffect statusEffect);
    IStatusEffect[] GetStatusEffects();

    //Interacting
    IInteractive GetInteractive();
    void CancelInteract();
    void Interact();
    
    //role
    Role GetRole();
    void SetRole(Role role);

    void ModSpeed(float mod);
    
    CharacterEvents GetCharacterEvents();
}

public class CharacterEvents{
    public UnityEvent onInventoryChange = new();
    public UnityEvent onHealthChange = new();
}