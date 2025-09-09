public interface IStatusEffect
{
    void OnApply(Player player);
    void OnUpdate(Player player);
    void OnRemove(Player player);
    bool CanAddCopy(Player player, IStatusEffect copy);
}