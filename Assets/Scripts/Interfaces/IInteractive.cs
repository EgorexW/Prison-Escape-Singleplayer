public interface IInteractive{
    public void Interact(Character character);

    public float HoldDuration { get; }
}

public struct DummyInteractive : IInteractive
{
    public readonly void Interact(Character character)
    {
        
    }

    public float HoldDuration => 0;
}