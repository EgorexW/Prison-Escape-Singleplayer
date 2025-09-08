using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(Item))]
public abstract class ItemEffect : MonoBehaviour, IItemEffect
{
    [GetComponent][SerializeField] Item item;
    public Item Item => item;
    
    public virtual void Use(Player player, bool alternative = false)
    {
        
    }
    public virtual void HoldUse(Player player, bool alternative = false)
    {
        
    }
    public virtual void StopUse(Player player, bool alternative = false)
    {
        
    }
}