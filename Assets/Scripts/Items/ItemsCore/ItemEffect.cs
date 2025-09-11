using UnityEngine;

[RequireComponent(typeof(Item))]
public abstract class ItemEffect : MonoBehaviour, IItemEffect
{
    public Item Item{ get; private set; }

    protected void Awake()
    {
        Item = GetComponent<Item>();
    }

    public virtual void Use(Player player, bool alternative = false) { }

    public virtual void HoldUse(Player player, bool alternative = false) { }

    public virtual void StopUse(Player player, bool alternative = false) { }
}