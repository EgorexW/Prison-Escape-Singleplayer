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
    
    [SerializeField] List<GameObject> targets;

    public List<GameObject> Targets => targets;

    public List<IAIObject> objects = new List<IAIObject>();

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