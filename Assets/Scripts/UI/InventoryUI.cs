using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [FormerlySerializedAs("defaultSprite")] [SerializeField] Sprite inventorySlotSprite;

    [SerializeField] GameObject prefab;
    readonly List<ItemUI> itemUIs = new();

    public void CreateitemUI()
    {
        var gameObjectTmp = Instantiate(prefab, transform);
        var itemUI = new ItemUI{
            gameObject = gameObjectTmp,
            image = gameObjectTmp.GetComponentInChildren<Image>(),
            aspectRatioFitter = gameObjectTmp.GetComponentInChildren<AspectRatioFitter>(),
            weightUI = gameObjectTmp.GetComponentInChildren<WeightUI>()
        };
        itemUIs.Add(itemUI);
    }

    public void ShowInventory(Player player)
    {
        var i = 0;
        var items = player.GetInventory().GetItems();
        var count = player.GetInventory().GetSize();
        var itemsCount = items.Count;
        while (itemUIs.Count < count) CreateitemUI();
        foreach (var itemUI in itemUIs){
            if (count <= i){
                itemUI.gameObject.SetActive(false);
                continue;
            }
            var sprite = inventorySlotSprite;
            bool highlighted = false;
            var weight = 0f;
            if (i < itemsCount){
                var item = items[i];
                sprite = item.GetPortrait();
                highlighted = player.GetHeldItem() == item;
                weight = item.Weight;
            }
            itemUI.gameObject.SetActive(true);
            itemUI.image.sprite = sprite;
            itemUI.image.color = highlighted ? Color.yellow : Color.white;
            itemUI.aspectRatioFitter.aspectRatio =
                sprite.bounds.extents.x / sprite.bounds.extents.y;
            itemUI.weightUI.ShowWeight(weight);
            i++;
        }
    }
}

public class ItemUI
{
    public AspectRatioFitter aspectRatioFitter;
    public GameObject gameObject;
    public Image image;
    public WeightUI weightUI;
}