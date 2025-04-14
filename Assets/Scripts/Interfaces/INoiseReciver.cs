using System;
using UnityEngine;

public interface INoiseReciver
{
    void ReceiveNoise(Noise noise);
}

[Serializable]
public struct Noise
{
    public float intensity;
    
    [HideInInspector] public GameObject source;
    [HideInInspector] public Vector3 pos;

    public Noise(int strenght)
    {
        intensity = strenght;
        source = null;
        pos = Vector3.zero;
    }
}