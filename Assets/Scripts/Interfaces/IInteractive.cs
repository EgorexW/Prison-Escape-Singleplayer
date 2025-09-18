public interface IInteractive
{
    public float HoldDuration{ get; }
    public void Interact(Player player);
    bool IsDummy => false;
}

public struct DummyInteractive : IInteractive
{
    public readonly void Interact(Player player) { }
    public bool IsDummy => true;

    public float HoldDuration => 0;
}