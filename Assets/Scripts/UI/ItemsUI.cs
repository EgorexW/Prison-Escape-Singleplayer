using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUI : MonoBehaviour
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
            aspectRatioFitter = gameObjectTmp.GetComponentInChildren<AspectRatioFitter>()
        };
        itemUIs.Add(itemUI);
    }

    public void ShowItems(SpriteUI[] items)
    {
        var i = 0;
        while (itemUIs.Count < items.Length) CreateitemUI();
        foreach (var item in itemUIs){
            if (items.Length <= i){
                item.gameObject.SetActive(false);
                continue;
            }
            item.gameObject.SetActive(true);
            if (items[i].sprite == null){
                items[i].sprite = defaultSprite;
            }
            item.image.sprite = items[i].sprite;
            item.image.color = items[i].highlighted ? Color.yellow : Color.white;
            item.aspectRatioFitter.aspectRatio =
                items[i].sprite.bounds.extents.x / items[i].sprite.bounds.extents.y;
            i++;
        }
    }
}

public class ItemUI
{
    public AspectRatioFitter aspectRatioFitter;
    public GameObject gameObject;
    public Image image;
}