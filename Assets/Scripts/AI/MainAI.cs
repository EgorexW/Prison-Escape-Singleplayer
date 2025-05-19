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
    [SerializeField] List<AITarget> targets;
    
    [SerializeField] float baseEnergy = 20;
    [SerializeField] float playerScoreMultiplier = 1;
    
    public List<AITarget> Targets => targets;

    public List<IAIObject> objects = new List<IAIObject>();

    float playerScore;

    public void AddObject(IAIObject aiObject)
    {
        aiObject.Init(this);
        objects.Add(aiObject);
    }

    public void RemoveObject(IAIObject aiObject)
    {
        objects.Remove(aiObject);
    }

    public void PlayerNoticed(float noticedScore)
    {
        playerScore += noticedScore;
    }
    
    public void ResolveObjects()
    {
        foreach (var aiObject in objects) aiObject.SetActive(false);
        objects.Shuffle();
        float localEnergy = CalculateEnergy();
        foreach (var aiObject1 in objects.Where(aiObject => aiObject.Stats.energyCost <= localEnergy)){
            aiObject1.SetActive(true);
            Debug.DrawRay(aiObject1.GameObject.transform.position, Vector3.up * 100, Color.red, 1);
            localEnergy -= aiObject1.Stats.energyCost;
        }
    }

    float CalculateEnergy()
    {
        return baseEnergy + playerScore * playerScoreMultiplier;
    }
}