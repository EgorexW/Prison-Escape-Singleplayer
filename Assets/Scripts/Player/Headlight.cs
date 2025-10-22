using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Headlight : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Light headlightLight;

    void Awake()
    {
        headlightLight.enabled = false;
    }

    public void ApplySettings(HeadlightSettings headlightSettings)
    {
        headlightLight.enabled = true;
        headlightLight.innerSpotAngle = headlightSettings.innerAngle;
        headlightLight.spotAngle = headlightSettings.outerAngle;
        headlightLight.intensity = headlightSettings.intensity;
        headlightLight.range = headlightSettings.range;
    }
}

[Serializable]
public class HeadlightSettings
{
    public float innerAngle = 22;
    public float outerAngle = 30;
    public float intensity = 2;
    public float range = 15;
}