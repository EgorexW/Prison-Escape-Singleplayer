using Sirenix.OdinInspector;
using UnityEngine;

public class RoomNode : LevelNode
    {
        [BoxGroup("References")][Required][SerializeField] public Room room;

        public override NodeType type => NodeType.Room;
        
        public void OnTriggerEnter(Collider other)
        {
            if (room == null){
                Debug.LogWarning("Room node has no room assigned", this);
                return;
            }
            if (other.GetComponent<Player>() != null){
                GameManager.i.roomsManager.PlayerEnteredRoom(room);
            }
        }
    }