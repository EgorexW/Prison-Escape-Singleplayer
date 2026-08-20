using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(Door))]
public class DoorLock : PoweredDevice, IInteractive
{
    [GetComponent] public Door door;

    public float resistance = 1;

    public DoorLockState state;

    public void Interact(Player player)
    {
        if (!IsPowered()){
            return;
        }
        if (state == DoorLockState.Unlocked){
            door.ChangeState();
        }
    }

    public float HoldDuration => 0;

    public void Unlock()
    {
        state = DoorLockState.Unlocked;
        door.Open();
    }

    public void Break()
    {
        state = DoorLockState.Broken;
        door.Open();
    }

    public void Lock()
    {
        if (state == DoorLockState.Broken){
            return;
        }
        state = DoorLockState.Locked;
        door.Close();
    }
}

public enum DoorLockState
{
    Locked,
    Unlocked,
    Broken
}