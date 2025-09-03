using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(GameDirector))]
public class AIInit : MonoBehaviour
{
    [SerializeField] float initDelay = 2;
    
    [FormerlySerializedAs("aiDirector")] [SerializeField][GetComponent] AIDirectorObsolete gameDirector;
    
    [SerializeField][Required] LevelNodes nodes;
    void Start()
    {
        General.CallAfterSeconds(Init, initDelay);
    }

    void Init()
    {
        foreach (var spawner in GetComponentsInChildren<IAISpawner>()){
            spawner.Spawn(nodes.CorridorNodes.Copy());
        }
        gameDirector.ResolveObjects();
    }
}

interface IAISpawner
{
    void Spawn(List<LevelNode> levelNodes);
}
