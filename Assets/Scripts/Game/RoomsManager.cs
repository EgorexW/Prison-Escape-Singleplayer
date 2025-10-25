using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class RoomsManager : MonoBehaviour
{
    [FoldoutGroup("Events")] public UnityEvent<Room> onPlayerEnteredRoom;

    public void PlayerEnteredRoom(Room room)
    {
        room.discovered = true;
        onPlayerEnteredRoom.Invoke(room);
    }
}