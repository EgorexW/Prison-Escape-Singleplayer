public interface IStatusEffect
{
    void OnApply(Character character);
    void OnUpdate(Character character);
    void OnRemove(Character character);
    bool CanAddCopy(Character character, IStatusEffect copy);
}
