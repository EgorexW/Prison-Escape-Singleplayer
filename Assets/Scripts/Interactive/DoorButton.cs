using UnityEngine;
using UnityEngine.Serialization;

public class DoorButton : MonoBehaviour, IInteractive
{
    [FormerlySerializedAs("interactive")] [SerializeField] Door door;
    public void Interact(Character character)
    {
        door.Interact(character);
    }
    public float GetHoldDuration(){
        return 0;
    }
}
