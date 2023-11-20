public interface IInteractive{
    public void Interact(ICharacter character);
    public float GetHoldDuration();
}

public struct DummyInteractive : IInteractive
{
    public readonly void Interact(ICharacter character)
    {
        
    }
    public readonly float GetHoldDuration(){
        return 0;
    }
}