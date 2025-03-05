public interface IKeycard{
    public bool HasAccess(AccessLevel requestedAccessLevel);
    public bool CanOpenWhenHeld();
}