using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] RoomGenerator roomGenerator;
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    
    void Start()
    {
        roomGenerator.Generate();
        var spawn = FindAnyObjectByType<PlayerSpawn>();
        spawn.Spawn(player);
    }
}
