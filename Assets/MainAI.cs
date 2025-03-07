using System.Collections.Generic;
using UnityEngine;

public class MainAI : MonoBehaviour
{
    [SerializeField] List<GameObject> targets;
    
    public List<GameObject> Targets => targets;

    public void AddObject(IAIObject aiObject)
    {
        aiObject.Init(this);
    }
}