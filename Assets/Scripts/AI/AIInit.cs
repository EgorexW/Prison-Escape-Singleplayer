using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AIDirector))]
public class AIInit : MonoBehaviour
{
    [SerializeField] float initDelay = 2;
    
    [FormerlySerializedAs("mainAI")] [SerializeField][GetComponent] AIDirector aiDirector;
    
    [SerializeField][Required] LevelNodes nodes;
    void Start()
    {
        General.CallAfterSeconds(Init, initDelay);
    }

    void Init()
    {
        foreach (var spawner in GetComponentsInChildren<IAISpawner>()){
            spawner.Spawn(nodes.CorridorNodes.Copy(), aiDirector);
        }
        aiDirector.ResolveObjects();
    }
}

interface IAISpawner
{
    void Spawn(List<LevelNode> levelNodes, AIDirector aiDirector);
}
