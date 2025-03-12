using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(MainAI))]
public class AIInit : MonoBehaviour
{
    [SerializeField] float initDelay = 2;
    
    [SerializeField][GetComponent] MainAI mainAI;
    
    [SerializeField][Required] LevelNodes nodes;
    void Start()
    {
        General.CallAfterSeconds(Init, initDelay);
    }

    void Init()
    {
        foreach (var spawner in GetComponentsInChildren<AISpawner>()){
            spawner.Spawn(nodes.CorridorNodes, mainAI);
        }
    }
}
