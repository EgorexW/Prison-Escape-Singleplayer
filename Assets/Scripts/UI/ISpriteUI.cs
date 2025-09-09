using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SpriteUI : ISpriteUI
{
    public Sprite sprite;
    public UnityAction callback;

    public SpriteUI(Sprite sprite, UnityAction value)
    {
        this.sprite = sprite;
        callback = value;
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