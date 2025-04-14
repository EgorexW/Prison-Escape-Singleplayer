using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class AIPlayerMarking : MonoBehaviour
{
    [SerializeField] float defaultErrorRadius = 200;
    [SerializeField] float markIntensityNoise = 1;
    [SerializeField] float minErrorRadius = 50;
    
    [ShowInInspector] List<PlayerMark> playerMarks = new List<PlayerMark>();

    [FoldoutGroup("Events")]
    public UnityEvent<PlayerApproximatePos> onPlayerApproximatePosChanged;

    [FoldoutGroup("Events")] public UnityEvent<PlayerMark> onPlayerMarkAdded;


    [ShowInInspector] public PlayerApproximatePos LastApproximatePos{ get; private set; } = PlayerApproximatePos.Global;
    
    const int UPDATEMARKSINTERVAL = 100;

    public void Update()
    {
        if (Time.frameCount % UPDATEMARKSINTERVAL == 0){
            UpdatePlayerMarks();
        }
    }

    public void PlayerNoticed(PlayerMark playerMark)
    {
        playerMarks.Add(playerMark);
        onPlayerMarkAdded.Invoke(playerMark);
        UpdatePlayerMarks();
    }

    void UpdatePlayerMarks()
    {
        playerMarks.RemoveAll(mark => !mark.Active);
        LastApproximatePos = GetPlayerApproximatePos();
        onPlayerApproximatePosChanged.Invoke(LastApproximatePos);
    }

    PlayerApproximatePos GetPlayerApproximatePos()
    {
        Vector3 weightedPosition = Vector3.zero;
        float totalIntensity = 0;
        
        foreach (var mark in playerMarks)
        {
            weightedPosition += mark.position * mark.CurrentIntensity;
            totalIntensity += mark.CurrentIntensity;
        }

        if (totalIntensity > 0){
            weightedPosition /= totalIntensity;
        }

        weightedPosition.y = 0;
        
        return new PlayerApproximatePos
        {
            pos = weightedPosition,
            totalIntensity = totalIntensity,
            errorRadius = Mathf.Max(defaultErrorRadius / totalIntensity, minErrorRadius)
        };
    }

    public void PlayerMadeNoise(Noise arg0)
    {
        PlayerNoticed(new PlayerMark(arg0.pos, arg0.intensity * markIntensityNoise));
    }
}

[Serializable]
public struct PlayerApproximatePos
{
    public static PlayerApproximatePos Global = new PlayerApproximatePos()
    {
        pos = Vector3.zero,
        totalIntensity = 0,
        errorRadius = float.MaxValue
    };
    public Vector3 pos;
    public float totalIntensity;
    public float errorRadius;
}

[Serializable]
public class PlayerMark
{
    const float DEFAULTACTIVETIME = 30f;
    
    public Vector3 position;
    public float intensity;
    public float time;
    public float activeTime;
    
    public PlayerMark(Vector3 pos, float intensity = 1f, float activeTime = DEFAULTACTIVETIME)
    {
        position = pos;
        time = Time.time;
        this.intensity = intensity;
        this.activeTime = activeTime;
    }
    
    public bool Active => timeSince < activeTime;
    [ShowInInspector] public float timeSince => Time.time - time;
    [ShowInInspector] public float CurrentIntensity => intensity * (activeTime - timeSince) / activeTime;

    public static PlayerMark Inactive = new PlayerMark(Vector3.zero){
        time = float.MaxValue,
        activeTime = 0,
        intensity = 0
    };
}