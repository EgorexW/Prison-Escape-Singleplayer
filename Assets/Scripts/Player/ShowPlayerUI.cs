using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

[RequireComponent(requiredComponent: typeof(ICharacter))]
public class ShowPlayerUI : NetworkBehaviour
{
    ICharacter character;
    GameObject playerStuff;
    ItemsUI itemsUI;
    HealthBarUI healthBarUI;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!IsOwner){
            return;
        }
        GetReferences();
        // Debug.Log("Awake Called");
        character.GetCharacterEvents().onInventoryChange.AddListener(ShowInventory);
        character.GetCharacterEvents().onHealthChange.AddListener(ShowHealth);

        void GetReferences()
        {
            // Debug.Log("GetReferences");
            playerStuff = GameObject.FindGameObjectWithTag("PlayerStuff");
            itemsUI = playerStuff.GetComponentInChildren<ItemsUI>();
            healthBarUI = playerStuff.GetComponentInChildren<HealthBarUI>();
            character = GetComponent<ICharacter>();
        }
    }
    // void Update(){
        // Debug.Log("Character", character.GetTransform());
    // }
    private void ShowHealth()
    {
        healthBarUI.SetHealth(character.GetHealth());
    }

    void ShowInventory(){
        List<IItem> items = character.GetInventory().GetItems();
        List<ISpriteUI> itemUIs = new();
        foreach (IItem item in items)
        {
            itemUIs.Add(new SpriteUI(item.GetPortrait(), () => character.EquipItem(item)));
        }
        itemsUI.ShowItems(items: itemUIs.ToArray());
    }
}
