using Sirenix.OdinInspector;
using UnityEngine;

public class RoomDiscoverer : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Room room;
    public void OnTriggerEnter(Collider other)
    {
        if (room == null){
            Debug.LogWarning("Room node has no room assigned", this);
            return;
        }
        if (other.GetComponent<Player>() != null){
            room.discovered = true;
        }
    }
}