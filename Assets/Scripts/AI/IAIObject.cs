public interface IAIObject
{
    AIObjectType aiType{ get; }
    
    void Init(MainAI mainAI);
}

public enum AIObjectType
{
    Turret,
    Trap
}