using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class WorldInit : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] LevelNodes levelNodes;
    [BoxGroup("References")] [Required] [SerializeField] RoomGenerator roomGenerator;
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    [BoxGroup("References")] [SerializeField] List<CorridorSpawner> corridorSpawners;

    [SerializeField] float delay = 0.5f;

    [FoldoutGroup("Events")]
    public UnityEvent onFinish;

    void Awake()
    {
        General.CallAfterSeconds(Generate, delay);
    }

    public void Generate()
    {
        roomGenerator.Generate();
        General.CallAfterSeconds(Generate2, delay);

        void Generate2()
        {
            levelNodes.ResetNodes();
            foreach (var corridorSpawner in corridorSpawners) corridorSpawner.Spawn(levelNodes.CorridorNodes);
            var spawn = FindAnyObjectByType<PlayerSpawn>();
            spawn.Spawn(player);
            onFinish.Invoke();
        }
    }
}