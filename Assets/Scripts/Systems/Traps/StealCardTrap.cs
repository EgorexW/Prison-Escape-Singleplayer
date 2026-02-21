using UnityEngine;

public class StealCardTrap : MonoBehaviour, ITrap
{
    public void SetRoom(Room room)
    {
        var readers = room.doorway.GetKeycardReaders();
        foreach (var reader in readers){
            reader.StealKeycard = true;
        }
    }

    public bool Eligable(Room room)
    {
        return room.doorway.accessLevel != null;
    }
}