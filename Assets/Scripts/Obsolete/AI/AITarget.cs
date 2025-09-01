using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class AITarget : MonoBehaviour, INoiseReciver
{
    [FoldoutGroup("Events")]
    public UnityEvent<Noise> onReceiveNoise;

    public UnityEvent<Discovery> onReceiveDiscovery;
    

    public static implicit operator GameObject(AITarget aiTarget)
    {
        return aiTarget.gameObject;
    }

    public void ReceiveNoise(Noise noise)
    {
        noise.source = gameObject;
        onReceiveNoise?.Invoke(noise);
    }

    public void ReceiveDiscovery(Discovery discovery)
    {
        
    }
}

[Serializable]
public class Discovery
{
    public float score;
}