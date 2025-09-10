using System;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(Item))]
public abstract class ItemEffect : MonoBehaviour, IItemEffect
{
    protected void Awake()
    {
        Item = GetComponent<Item>();
    }

    public Item Item{ get; private set; }

    public virtual void Use(Player player, bool alternative = false) { }

    public virtual void HoldUse(Player player, bool alternative = false) { }

    public virtual void StopUse(Player player, bool alternative = false) { }
}