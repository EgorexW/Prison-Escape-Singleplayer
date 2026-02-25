using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class RoomNode : LevelNode
{
    [BoxGroup("References")] [Required] [SerializeField] public Room room;

    [FoldoutGroup("Events")]
    public UnityEvent<Room> onPlayerEnteredRoom;

    public override NodeType type => NodeType.Room;

    public void OnTriggerEnter(Collider other)
    {
        if (room == null){
            Debug.LogWarning("Room node has no room assigned", this);
            return;
        }
        if (other.GetComponent<Player>() != null){
            room.discovered = true;
            onPlayerEnteredRoom.Invoke(room);
            GameManager.i.levelNodes.onPlayerEnteredRoom.Invoke(room);
        }
    }
}