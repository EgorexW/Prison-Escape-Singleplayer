using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public void Spawn(Player player)
    {
        player.SetPos(transform.position);
        player.transform.rotation = transform.rotation;
    }
}