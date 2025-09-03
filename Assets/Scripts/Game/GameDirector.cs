using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameDirector : SerializedMonoBehaviour
{
    public static GameDirector i { get; private set; }

    [BoxGroup("References")] [Required] [SerializeField] Player player;
    
    public Player Player => player;

    [SerializeField] bool log;
    
    [SerializeField] float playerScoreMultiplier = 1;

    [ShowInInspector] float playerScore;
    

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
    
    public void PlayerDiscovery(Discovery discovery)
    {
        Log("Player discovery " + discovery.score);
        playerScore += discovery.score;
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