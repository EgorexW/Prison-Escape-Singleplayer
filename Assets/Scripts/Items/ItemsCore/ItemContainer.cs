using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemContainer : MonoBehaviour, IInteractive
{
    [SerializeField] [RequireInterface(typeof(IItem))] Object item;
    [SerializeField] float holdDuration = 1f;

    IItem Item {
        get {
            if (item is ScriptableObject){
                return (IItem)item;
            }
            if (item is Component component)
            {
                return component.GetComponent<IItem>();
            }
            if (item is GameObject gameObject)
            {
                return gameObject.GetComponent<IItem>();
            }
            return null;
        }
    }

    public void Interact(ICharacter character)
    {
        if (Item == null){
            item = GetComponent<ItemBase>();
        }
        if (Item == null){
            return;
        }
        character.PickupItem(Item);
    }

    public void Interact()
    {
        
    }
    public float GetHoldDuration(){
        return holdDuration;
    }
}