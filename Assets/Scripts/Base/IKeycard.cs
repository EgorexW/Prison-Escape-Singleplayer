public interface IKeycard
{
    bool OneUse{ get; }
    public bool HasAccess(AccessLevel requestedAccessLevel);
}