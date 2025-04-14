using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class AITarget : MonoBehaviour, INoiseReciver
{
    [FoldoutGroup("Events")]
    public UnityEvent<Noise> onReceiveNoise;

    public static implicit operator GameObject(AITarget aiTarget)
    {
        return aiTarget.gameObject;
    }

    public void ReceiveNoise(Noise noise)
    {
        // Debug.Log("Noise received: " + noise.intensity, gameObject);
        noise.source = gameObject;
        onReceiveNoise?.Invoke(noise);
    }
}