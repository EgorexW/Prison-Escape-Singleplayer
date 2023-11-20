using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Component.Transforming;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using NaughtyAttributes;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public partial class Character : NetworkBehaviour, ICharacter
{
    [SerializeField] Transform aim;
    CharacterEvents characterEvents = new CharacterEvents(); 
    [SyncVar] Role role;
    NetworkTransform netTransform;

    void Awake(){
        inventory = GetComponent<IInventory>();
        inventory.OnInventoryChange.AddListener(characterEvents.onInventoryChange.Invoke);
        netTransform = GetComponent<NetworkTransform>();
        characterController = GetComponent<CharacterController>();
        firstPersonController = GetComponent<FirstPersonController>();
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