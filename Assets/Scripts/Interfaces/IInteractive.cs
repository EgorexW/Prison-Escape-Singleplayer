public interface IInteractive{
    public void Interact(Player player);

    public float HoldDuration { get; }
}

public struct DummyInteractive : IInteractive
{
    public readonly void Interact(Player player)
    {
        
    }

    public float HoldDuration => 0;
}