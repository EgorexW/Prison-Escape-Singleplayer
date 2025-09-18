using System;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SetRandomSprite : MonoBehaviour
{
    [GetComponent][SerializeField] SpriteRenderer spriteRenderer;
    
    [SerializeField] List<Sprite> sprites;

    void Start()
    {
        spriteRenderer.sprite = sprites.Random();
    }
}
