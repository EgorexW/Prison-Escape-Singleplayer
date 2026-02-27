using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;

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
        var count = items.Count;
        while (itemUIs.Count < count) CreateitemUI();
        foreach (var itemUI in itemUIs){
            if (count <= i){
                itemUI.gameObject.SetActive(false);
                continue;
            }
            var item = items[i];
            var sprite = item.GetPortrait();
            itemUI.gameObject.SetActive(true);
            if (sprite == null){
                sprite = defaultSprite;
            }
            itemUI.image.sprite = sprite;
            bool highlighted = player.GetHeldItem() == item;
            itemUI.image.color = highlighted ? Color.yellow : Color.white;
            itemUI.aspectRatioFitter.aspectRatio =
                sprite.bounds.extents.x / sprite.bounds.extents.y;
            itemUI.weightUI.ShowWeight(item.Weight);
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