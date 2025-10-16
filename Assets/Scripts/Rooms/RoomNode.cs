using Sirenix.OdinInspector;
using UnityEngine;

public class RoomNode : LevelNode
    {
        [BoxGroup("References")][Required][SerializeField] public Room room;

        public override NodeType type => NodeType.Room;
    }