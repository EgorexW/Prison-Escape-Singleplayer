using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(Door))]
public class DoorLock : PoweredDevice, IInteractive
{
    [GetComponent] [SerializeField] public Door door;

    public float resistance = 1;

    public bool unlocked;

    public void Interact(Player player)
    {
        if (!IsPowered()){
            return;
        }
        if (unlocked){
            door.ChangeState();
        }
    }

    public float HoldDuration => 0;
    
    public void Unlock()
    {
        unlocked = true;
    }
}