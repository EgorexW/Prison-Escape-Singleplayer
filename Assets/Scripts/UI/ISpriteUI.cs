using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SpriteUI : ISpriteUI
{
    public UnityAction callback;
    public Sprite sprite;

    public SpriteUI(Sprite sprite, UnityAction value)
    {
        this.sprite = sprite;
        this.callback = value;
    }

    public UnityAction GetCallback()
    {
        return callback;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}
public interface ISpriteUI
{
    public Sprite GetSprite();
    public UnityAction GetCallback();
}