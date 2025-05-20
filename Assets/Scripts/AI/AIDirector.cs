using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class AIDirector : SerializedMonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    
    [SerializeField] float baseEnergy = 20;
    [SerializeField] float playerScoreMultiplier = 1;
    
    static float playerScore;

    static List<IAIObject> objects = new List<IAIObject>();
    
    
    public static void AddObject(IAIObject aiObject)
    {
        objects.Add(aiObject);
    }

    public static void RemoveObject(IAIObject aiObject)
    {
        objects.Remove(aiObject);
    }

    public static void PlayerNoticed(Discovery discovery)
    {
        playerScore += discovery.score;
    }
    
    public void ResolveObjects()
    {
        foreach (var aiObject in objects) aiObject.SetActive(false);
        objects.Shuffle();
        var localEnergy = CalculateEnergy();
        foreach (var aiObject1 in objects.Where(aiObject => aiObject.Stats.energyCost <= localEnergy)){
            aiObject1.SetActive(true);
            // Debug.DrawRay(aiObject1.GameObject.transform.position, Vector3.up * 100, Color.red, 1);
            localEnergy -= aiObject1.Stats.energyCost;
        }
    }

    float CalculateEnergy()
    {
        return baseEnergy + playerScore * playerScoreMultiplier;
    }

    public List<IAIObject> GetActiveAIObjects()
    {
        return objects.Where(aiObject => aiObject.IsActive).ToList();
    }
}