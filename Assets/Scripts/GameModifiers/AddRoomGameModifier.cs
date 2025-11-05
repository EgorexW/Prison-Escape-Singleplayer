using Sirenix.OdinInspector;
using UnityEngine;

public class AddRoomGameModifier : GameModifierSpecial
{
    [BoxGroup("References")] [Required] [SerializeField] RoomChooser roomChooser;

    [BoxGroup("References")] [Required] [SerializeField] GameObject room;

    public override void Apply()
    {
        roomChooser.necessaryRooms.Add(room);
    }
}