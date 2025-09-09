public interface IInteractive
{
    public float HoldDuration{ get; }
    public void Interact(Player player);
}

public struct DummyInteractive : IInteractive
{
    public readonly void Interact(Player player) { }

    public float HoldDuration => 0;
}