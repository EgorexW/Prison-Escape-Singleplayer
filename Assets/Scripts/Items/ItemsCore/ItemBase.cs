using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IItem
{
    List<IItemObserver> observers = new();

    void Awake(){
        observers = new(GetComponents<IItemObserver>());
        observers.Remove(this);
    }
    public virtual Sprite GetPortrait()
    {
        Texture2D tex = RuntimePreviewGenerator.GenerateModelPreview(transform);
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
        return sprite;
    }
    public virtual void OnDeequip(ICharacter character)
    {
        foreach (IItemObserver observer in observers)
        {    
            observer.OnDeequip(character);
        }
    }
    public virtual void Use(ICharacter character, bool alternative = false){
        foreach (IItemObserver observer in observers)
        {
            observer.Use(character, alternative);
        }
    }
    public virtual void OnDrop(ICharacter character, Vector3 force)
    {
        foreach (IItemObserver observer in observers)
        {
            observer.OnDrop(character, force);
        }
    }

    public virtual void OnEquip(ICharacter character)
    {
        foreach (IItemObserver observer in observers)
        {
            observer.OnEquip(character);
        }
    }

    public virtual void OnPickup(ICharacter character)
    {
        foreach (IItemObserver observer in observers)
        {
            observer.OnPickup(character);
        }
    }

    public virtual void HoldUse(ICharacter character, bool alternative = false)
    {
        foreach (IItemObserver observer in observers)
        {
            observer.HoldUse(character, alternative);
        }
    }

    public virtual void StopUse(ICharacter character, bool alternative = false)
    {
        foreach (IItemObserver observer in observers)
        {
            observer.StopUse(character, alternative);
        }
    }
}