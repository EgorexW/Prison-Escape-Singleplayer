using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public void Spawn(Player player)
    {
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;
    }
}