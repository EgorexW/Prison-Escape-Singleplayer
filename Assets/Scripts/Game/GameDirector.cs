using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameDirector : SerializedMonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    [BoxGroup("References")][Required] public GameTime gameTime;

    [SerializeField] bool log;

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

    #region Debug

    void Log(string msg)
    {
        if (log){
            Debug.Log(msg, this);
        }
    }

    #endregion
}