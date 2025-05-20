using System;
using UnityEngine;

public interface IAIObject
{
    public AIObjectStats Stats { get; }
    void SetActive(bool active);
    
    GameObject GameObject { get; }
    bool IsActive{ get; }
}

[Serializable]
public class AIObjectStats
{
    public float energyCost = 1;
}