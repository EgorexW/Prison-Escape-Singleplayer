using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

class PlayerUI : MonoBehaviour
{
    [GetComponentInChildren] [SerializeField] HealthBarUI healthBarUI;
    [FormerlySerializedAs("itemsUI")] [GetComponentInChildren] [SerializeField] InventoryUI inventoryUI;
    [GetComponentInChildren] [SerializeField] WeightUI weightUI;
    [FormerlySerializedAs("character")] [Required] [SerializeField] Player player;
    [Required] [SerializeField] MetricBar staminaBarUI;
    [Required] [SerializeField] MetricBar progressBarUI;
    [Required] [SerializeField] TextMeshProUGUI itemName;

    void Start()
    {
        player.onInventoryChange.AddListener(ShowInventory);
        player.playerHealth.onHealthChange.AddListener(ShowHealth);
        player.onHoldInteraction.AddListener((t, d) => progressBarUI.Set(t / d));
        player.onFinishInteraction.AddListener(() => progressBarUI.Hide());
        player.playerHealth.Health.onDamage.AddListener(ShowDamage);
        ShowHealth();
        ShowInventory();
        progressBarUI.Hide();
    }

    void Update()
    {
        staminaBarUI.Set(player.Stamina, 1);
        var heldItem = player.GetHeldItem();
        if (heldItem != null){
            itemName.text = heldItem.Name;
        }

        else{
            itemName.text = "";
        }
        itemName.gameObject.SetActive(itemName.text != "");
    }

    void ShowDamage(Damage damage)
    {
        healthBarUI.ShowDamage(damage, player.playerHealth.Health);
    }

    void ShowHealth()
    {
        healthBarUI.SetHealth(player.playerHealth.Health);
    }

    void ShowInventory()
    {
        inventoryUI.ShowInventory(player);
        weightUI.ShowWeight(player.GetInventory().GetWeight());
    }
}