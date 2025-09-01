using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class AIDirector : SerializedMonoBehaviour
{
    public static AIDirector i { get; private set; }

    [BoxGroup("References")] [Required] [SerializeField] Player player;
    
    public Player Player => player;

    [SerializeField] bool log;
    
    [SerializeField] float baseEnergy = 20;
    [SerializeField] float playerScoreMultiplier = 1;
    [SerializeField] float immediateUpdateThreshold = 5;
    [SerializeField] float timeBetweenObjectUpdates = 30;

    [ShowInInspector] float lastObjectUpdate;
    [ShowInInspector] float playerScore;

    List<IAIObject> objects = new List<IAIObject>();

    void Awake()
    {
        if (i != null && i != this){
            Debug.LogWarning("There was another instance", this);
            Destroy(gameObject);
            return;
        }
        i = this;
        // DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        TryObjectsUpdate();
    }

    void TryObjectsUpdate()
    {
        if (Time.time - lastObjectUpdate < timeBetweenObjectUpdates){
            return;
        }
        lastObjectUpdate = Time.time;
        LevelNode node = player.GetContainingNode();
        if (node.nodeType == NodeType.Room){
            Log("In a room node, updating objects");
            ResolveObjects();
        } else {
            Log("Not in a room node, not updating objects");
        }
    }

    public void AddObject(IAIObject aiObject)
    {
        objects.Add(aiObject);
    }

    public void RemoveObject(IAIObject aiObject)
    {
        objects.Remove(aiObject);
    }

    public void PlayerDiscovery(Discovery discovery)
    {
        Log("Player discovery " + discovery.score);
        playerScore += discovery.score;
        if (discovery.score > immediateUpdateThreshold){
            ResolveObjects();
        }
    }
    
    public void ResolveObjects()
    {
        lastObjectUpdate = Time.time;
        foreach (var aiObject in objects) aiObject.SetActive(false);
        objects.Shuffle();
        var localEnergy = CalculateEnergy();
        foreach (var aiObject1 in objects.Where(aiObject => aiObject.Stats.energyCost <= localEnergy)){
            aiObject1.SetActive(true);
            localEnergy -= aiObject1.Stats.energyCost;
            // Debug.DrawRay(aiObject1.GameObject.transform.position, Vector3.up * 100, Color.red, 1);
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

    #region Debug

    void Log(string msg)
    {
        if (log){
            Debug.Log(msg, this);
        }
    }

    #endregion
}