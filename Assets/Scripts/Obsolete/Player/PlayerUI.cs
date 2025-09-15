using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

class PlayerUI : MonoBehaviour
{
    [GetComponentInChildren] [SerializeField] HealthBarUI healthBarUI;
    [GetComponentInChildren] [SerializeField] ItemsUI itemsUI;
    [FormerlySerializedAs("character")] [Required] [SerializeField] Player player;
    [Required] [SerializeField] MetricBar staminaBarUI;
    [Required] [SerializeField] MetricBar progressBarUI;

    void Awake()
    {
        player.onInventoryChange.AddListener(ShowInventory);
        player.playerHealth.onHealthChange.AddListener(ShowHealth);
        player.onHoldInteraction.AddListener((t, d) => progressBarUI.Set(t / d));
        player.onFinishInteraction.AddListener(() => progressBarUI.Hide());

        ShowInventory();
        progressBarUI.Hide();
    }

    void Update()
    {
        staminaBarUI.Set(player.Stamina, 1);
    }

    void ShowHealth()
    {
        healthBarUI.SetHealth(player.playerHealth.Health);
    }

    void ShowInventory()
    {
        var items = player.GetInventory().GetItems();
        SpriteUI[] itemUIs = new SpriteUI[player.GetInventory().GetSize()];
        for (var i = 0; i < itemUIs.Length; i++)
            if (i < items.Count)
                itemUIs[i] = new SpriteUI(items[i].GetPortrait(), items[i] == player.GetHeldItem());
            else
                itemUIs[i] = new SpriteUI(null, false);
        itemsUI.ShowItems(itemUIs);
    }
}