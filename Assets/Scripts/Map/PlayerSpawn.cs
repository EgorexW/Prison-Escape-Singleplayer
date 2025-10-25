using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] bool zeroAscensionSpawn = true;

    public bool CanSpawn()
    {
        if (zeroAscensionSpawn){
            return Ascensions.AscensionLevel == 0;
        }
        return Ascensions.AscensionLevel != 0;
    }

    public void Spawn(Player player)
    {
        player.Spawn(transform.position, transform.rotation);
    }
}