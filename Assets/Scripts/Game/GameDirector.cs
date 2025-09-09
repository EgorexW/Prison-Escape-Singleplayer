using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameDirector : SerializedMonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Player player;

    [SerializeField] bool log;

    [SerializeField] float playerScoreMultiplier = 1;

    [ShowInInspector] float playerScore;
    public static GameDirector i{ get; private set; }

    public Player Player => player;


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

[Serializable]
public class Discovery
{
    public float score;
}