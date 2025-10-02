using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class GameInit : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] LevelNodes levelNodes;
    [BoxGroup("References")] [Required] [SerializeField] RoomGenerator roomGenerator;
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    [BoxGroup("References")] [SerializeField] List<CorridorSpawner> corridorSpawners;

    const float DELAY = 1f;

    [FoldoutGroup("Events")] public UnityEvent onFinish;

    void Awake()
    {
        General.CallAfterSeconds(Generate, DELAY);
    }

    public void Generate()
    {
        roomGenerator.Generate();
        General.CallAfterSeconds(Generate2, DELAY);

        void Generate2()
        {
            levelNodes.ResetNodes();
            foreach (var corridorSpawner in corridorSpawners) corridorSpawner.Spawn(levelNodes.CorridorNodes);
            var spawn = FindAnyObjectByType<PlayerSpawn>();
            if (spawn == null){
                Debug.LogWarning("No PlayerSpawn found in scene", this);
                General.CallAfterSeconds(Generate2, DELAY);
                return;
            }
            spawn.Spawn(player);
            levelNodes.ResetNodes();
            onFinish.Invoke();
        }
    }
}