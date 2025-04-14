using System;
using System.Collections.Generic;
using System.Linq;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(AIPlayerMarking), typeof(AIObjects))]
public class MainAI : SerializedMonoBehaviour
{
    [GetComponent] public AIPlayerMarking aiPlayerMarking;
    [GetComponent] public AIObjects aiObjects;
    
    [SerializeField] List<AITarget> targets;

    [SerializeField] float intensityForImmediateObjectsReset = 3;
    
    public List<AITarget> Targets => targets;

    public List<IAIObject> objects = new List<IAIObject>();

    void Awake()
    {
        aiPlayerMarking.onPlayerMarkAdded.AddListener(OnPlayerMarkAdded);
        foreach (var target in targets){
            target.onReceiveNoise.AddListener(aiPlayerMarking.PlayerMadeNoise);
        }
    }
    void OnPlayerMarkAdded(PlayerMark arg0)
    {
        if (aiPlayerMarking.LastApproximatePos.totalIntensity > intensityForImmediateObjectsReset){
            aiObjects.ResetObjects(objects, aiPlayerMarking.LastApproximatePos);
        }
    }

    protected void Update()
    {
        aiPlayerMarking.Update();
    }

    public void AddObject(IAIObject aiObject)
    {
        aiObject.Init(this);
        objects.Add(aiObject);
    }

    public void RemoveObject(IAIObject aiObject)
    {
        objects.Remove(aiObject);
    }
}