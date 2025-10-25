public class DiscoverAllRoomsTrigger : FacilityTrigger
{
    public override void Activate()
    {
        base.Activate();
        var rooms = GetComponentsInChildren<Room>();
        foreach (var room in rooms) room.discovered = true;
    }
}