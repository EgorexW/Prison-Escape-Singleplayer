using System;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

class PlayerUI : MonoBehaviour
{
    [Required] [SerializeField] Character character;

    [GetComponentInChildren][SerializeField] HealthBarUI healthBarUI;
    [GetComponentInChildren][SerializeField] ItemsUI itemsUI;
    [Required][SerializeField] MetricBar staminaBarUI;

    void Awake()
    {
        character.GetCharacterEvents().onInventoryChange.AddListener(ShowInventory);
        character.GetCharacterEvents().onHealthChange.AddListener(ShowHealth);
    }

    void Update()
    {
        staminaBarUI.Set(character.Stamina, 1);
    }

    private void ShowHealth()
    {
        healthBarUI.SetHealth(character.Health);
    }

    void ShowInventory(){
        var items = character.GetInventory().GetItems();
        List<ISpriteUI> itemUIs = new();
        foreach (Item item in items)
        {
            itemUIs.Add(new SpriteUI(item.GetPortrait(), () => character.EquipItem(item)));
        }
        itemsUI.ShowItems(items: itemUIs.ToArray());
    }
}