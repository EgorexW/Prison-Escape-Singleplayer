public interface IKeycard
{
    public bool HasAccess(AccessLevel requestedAccessLevel);
    bool OneUse{ get; }
}