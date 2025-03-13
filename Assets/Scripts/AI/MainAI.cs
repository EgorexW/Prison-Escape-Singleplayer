using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class MainAI : SerializedMonoBehaviour
{
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
        if (lastAwarenessIncreaseTime[aiObject] != 0 || Time.time - lastAwarenessIncreaseTime[aiObject] < awarenessIncreaseResetTime){
            playerAwareness += awarenessIncreaseForType[aiObject.aiType];
        }
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

    void AggroOnPlayer()
    {
        
    }
}