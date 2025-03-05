public interface IDoor : IInteractive
{
    bool CanCharacterUse(Character character, bool v);
    void LockState(bool v);
    void Open();
}
