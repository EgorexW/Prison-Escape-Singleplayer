using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Door))]
public class DoorLock : MonoBehaviour, IInteractive
{
    [GetComponent][SerializeField] Door door;
    
    public bool unlocked = false;
    public void Interact(Player player)
    {
        if (unlocked)
        {
            door.ChangeState();
        }
    }

    public float HoldDuration => 0;
}
