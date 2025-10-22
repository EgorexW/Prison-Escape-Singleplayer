public abstract class Equipment : Loot
{
    public override void Interact(Player player)
    {
        Apply(player);
        base.Interact(player);
        Destroy(gameObject);
    }

    protected abstract void Apply(Player player);
}