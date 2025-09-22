public interface IInteractive
{
    public float HoldDuration{ get; }
    bool IsDummy => false;
    public void Interact(Player player);
}

public struct DummyInteractive : IInteractive
{
    public readonly void Interact(Player player) { }
    public bool IsDummy => true;

    public float HoldDuration => 0;
}