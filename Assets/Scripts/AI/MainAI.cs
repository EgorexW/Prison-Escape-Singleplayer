using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AIAggro))]
public class MainAI : SerializedMonoBehaviour
{
    [SerializeField][GetComponent] AIAggro aiPlayerAggro;
    
    [SerializeField] List<GameObject> targets;

    [SerializeField] Dictionary<AIObjectType, float> awarenessIncreaseForType = new();
    [SerializeField] float awarenessIncreaseResetTime = 60;
    [SerializeField] float awarenessReset = 0.8f;

    [SerializeField] float playerAwareness;

    Dictionary<IAIObject, float> lastAwarenessIncreaseTime;

    public List<GameObject> Targets => targets;

    public void AddObject(IAIObject aiObject)
    {
        aiObject.Init(this);
    }

    public void IncreaseAwareness(IAIObject aiObject)
    {
        if (lastAwarenessIncreaseTime.ContainsKey(aiObject)){
            if (Time.time - lastAwarenessIncreaseTime[aiObject] < awarenessIncreaseResetTime){
                return;
            }
        }
        playerAwareness += awarenessIncreaseForType[aiObject.aiType];
        lastAwarenessIncreaseTime[aiObject] = Time.time;
        ResolveAwarenessChange();
    }

    void ResolveAwarenessChange()
    {
        if (playerAwareness >= 1){
            AggroOnPlayer();
            playerAwareness -= awarenessReset;
        }
    }

    [Button]
    void AggroOnPlayer()
    {
        aiPlayerAggro.Aggro(targets);
    }
}