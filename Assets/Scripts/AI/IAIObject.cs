using System;
using UnityEngine;

public interface IAIObject
{
    // AIObjectType aiType{ get; }
    
    void Init(MainAI mainAI);
    
    public AIObjectStats Stats { get; }
    void SetActive(bool active);
    
    GameObject GameObject { get; }
}

[Serializable]
public class AIObjectStats
{
    public float energyCost = 1;
}

public enum AIObjectType
{
    Turret,
    Trap
}