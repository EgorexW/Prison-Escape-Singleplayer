using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Door2))]
public class DoorInteract : MonoBehaviour, IInteractive
{
    [GetComponent][SerializeField] Door2 door;
    
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
