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
    [Required][SerializeField] MetricBar progressBarUI;

    void Awake()
    {
        character.onInventoryChange.AddListener(ShowInventory);
        character.onHealthChange.AddListener(ShowHealth);
        character.onHoldInteraction.AddListener((t, d) => progressBarUI.Set(t/d));
        character.onFinishInteraction.AddListener(() => progressBarUI.Hide());
        
        progressBarUI.Hide();
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
        foreach (var item in items)
        {
            itemUIs.Add(new SpriteUI(item.GetPortrait(), () => character.EquipItem(item)));
        }
        itemsUI.ShowItems(items: itemUIs.ToArray());
    }
}
