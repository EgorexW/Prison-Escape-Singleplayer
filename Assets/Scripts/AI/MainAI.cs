using System;
using System.Collections.Generic;
using System.Linq;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MainAI : SerializedMonoBehaviour
{
    [GetComponent] public AIPlayerMarking aiPlayerMarking;
    
    [SerializeField] List<GameObject> targets;

    public List<GameObject> Targets => targets;

    List<IAIObject> objects = new List<IAIObject>();

    protected void Update()
    {
        aiPlayerMarking.Update();
    }

    public void AddObject(IAIObject aiObject)
    {
        aiObject.Init(this);
        objects.Add(aiObject);
    }
    
    public void Init()
    {
                
    }
}


