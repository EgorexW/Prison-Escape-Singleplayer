public interface IInteractive{
    public void Interact(Character character);

    public float GetHoldDuration()
    {
        return 1;
    }
}

public struct DummyInteractive : IInteractive
{
    public readonly void Interact(Character character)
    {
        
    }
    public readonly float GetHoldDuration(){
        return 0;
    }
}