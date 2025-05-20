using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUI : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    List<ItemUI> itemUIs = new();

    public void CreateitemUI(){
        var gameObjectTmp = Instantiate(prefab, transform);
        var itemUI = new ItemUI()
        {
            gameObject = gameObjectTmp,
            image = gameObjectTmp.GetComponentInChildren<Image>(),
            button = gameObjectTmp.GetComponentInChildren<Button>(),
            aspectRatioFitter = gameObjectTmp.GetComponentInChildren<AspectRatioFitter>()
        };
        itemUIs.Add(itemUI);
    }
    public void ShowItems(ISpriteUI[] items){
        var i = 0;
        while(itemUIs.Count < items.Length){
            CreateitemUI();
        }
        foreach (var item in itemUIs)
        {
            if (items.Length <= i){
                item.gameObject.SetActive(false);
                continue;
            }
            item.gameObject.SetActive(true);
            item.image.sprite = items[i].GetSprite();
            item.aspectRatioFitter.aspectRatio = items[i].GetSprite().bounds.extents.x/items[i].GetSprite().bounds.extents.y;
            item.button.onClick.RemoveAllListeners();
            item.button.onClick.AddListener(items[i].GetCallback());
            i++;
        }
    }
}

public class ItemUI{
    public Image image;
    public AspectRatioFitter aspectRatioFitter;
    public GameObject gameObject;
    public Button button;
}