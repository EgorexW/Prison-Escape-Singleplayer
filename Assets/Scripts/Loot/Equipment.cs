public abstract class Equipment : Loot
{
    protected bool destroyOnUse = true;
    
    public override void Interact(Player player)
    {
        Apply(player);
        base.Interact(player);
        if (destroyOnUse){
            Destroy(gameObject);
        }
    }

    protected abstract void Apply(Player player);
}