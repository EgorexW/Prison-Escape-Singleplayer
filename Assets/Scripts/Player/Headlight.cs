using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Headlight : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Light headlightLight;
    
    bool unlocked = false;

    void Awake()
    {
        headlightLight.enabled = false;
    }

    public void ApplySettings(HeadlightSettings headlightSettings)
    {
        unlocked = headlightSettings.unlocked;
        headlightLight.enabled = unlocked;
        headlightLight.innerSpotAngle = headlightSettings.innerAngle;
        headlightLight.spotAngle = headlightSettings.outerAngle;
        headlightLight.intensity = headlightSettings.intensity;
        headlightLight.range = headlightSettings.range;
    }

    public void Toggle()
    {
        if (!unlocked){
            return;
        }
        headlightLight.enabled = !headlightLight.enabled;
    }
}

[Serializable]
public class HeadlightSettings
{
    public bool unlocked = true;
    public float innerAngle = 22;
    public float outerAngle = 30;
    public float intensity = 2;
    public float range = 15;
}