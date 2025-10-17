using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public void Spawn(Player player)
    {
        player.Spawn(transform.position, transform.rotation);
    }
}