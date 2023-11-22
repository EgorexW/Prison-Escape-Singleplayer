using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public partial class Character : MonoBehaviour, ICharacter
{
    [SerializeField] Transform aim;
    CharacterEvents characterEvents = new CharacterEvents(); 
    [SerializeField] Role role;

    void Awake(){
        inventory = GetComponent<IInventory>();
        inventory.OnInventoryChange.AddListener(characterEvents.onInventoryChange.Invoke);
        characterController = GetComponent<CharacterController>();
        firstPersonController = GetComponent<IMover>();
        SetFirstPersonController();
    }
    public IStatusEffect[] GetStatusEffects(){
        return statusEffects.ToArray();
    }

    public CharacterEvents GetCharacterEvents()
    {
        return characterEvents;
    }

    public Transform GetAimTransform()
    {
        return aim;
    }
    public Role GetRole(){
        return role;
    }
    public void SetRole(Role role){
        this.role = role;
    }
}