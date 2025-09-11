using System;
using UnityEngine;

[Serializable]
public class SpriteUI
{
    public Sprite sprite;
    public bool highlighted;

    public SpriteUI(Sprite sprite, bool highlighted)
    {
        this.sprite = sprite;
        this.highlighted = highlighted;
    }
}