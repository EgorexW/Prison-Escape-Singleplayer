using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

class PlayerUI : MonoBehaviour
{
    [GetComponentInChildren] [SerializeField] HealthBarUI healthBarUI;
    [GetComponentInChildren] [SerializeField] ItemsUI itemsUI;
    [FormerlySerializedAs("character")] [Required] [SerializeField] Player player;
    [Required] [SerializeField] MetricBar staminaBarUI;
    [Required] [SerializeField] MetricBar progressBarUI;
    [Required] [SerializeField] TextMeshProUGUI itemName;
    [Required] [SerializeField] TextMeshProUGUI announcementText;

    [SerializeField] float announcmentShowTime = 5f;

    void Awake()
    {
        player.onInventoryChange.AddListener(ShowInventory);
        player.playerHealth.onHealthChange.AddListener(ShowHealth);
        player.onHoldInteraction.AddListener((t, d) => progressBarUI.Set(t / d));
        player.onFinishInteraction.AddListener(() => progressBarUI.Hide());
        announcementText.gameObject.SetActive(false);
        player.playerHealth.Health.onDamage.AddListener(ShowDamage);
    }

    void Start()
    {
        GameManager.i.facilityAnnouncements.onAnnouncement.AddListener(ShowAnnouncement);
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

    void ShowAnnouncement(FacilityAnnouncement announcement)
    {
        announcementText.text = announcement.message;
        announcementText.gameObject.SetActive(true);
        LeanTween.delayedCall(announcmentShowTime, () => announcementText.gameObject.SetActive(false));
    }

    void ShowHealth()
    {
        healthBarUI.SetHealth(player.playerHealth.Health);
    }

    void ShowInventory()
    {
        var items = player.GetInventory().GetItems();
        var itemUIs = new SpriteUI[player.GetInventory().GetSize()];
        for (var i = 0; i < itemUIs.Length; i++)
            if (i < items.Count){
                itemUIs[i] = new SpriteUI(items[i].GetPortrait(), items[i] == player.GetHeldItem());
            }
            else{
                itemUIs[i] = new SpriteUI(null, false);
            }
        itemsUI.ShowItems(itemUIs);
    }
}