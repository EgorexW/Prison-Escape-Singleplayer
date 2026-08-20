using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class AmbienceMusic : MonoBehaviour
{
    [SerializeField][BoxGroup("Sounds")] AudioSource power;
    [SerializeField][BoxGroup("Sounds")] AudioSource lackOfPower;
    [SerializeField][BoxGroup("Sounds")] AudioSource secureWing;

    [Range(0, 1)] public float maxVolume = 1f;
    [SerializeField][Min(0.01f)] float fadeTime = 5f;
    
    bool IsInSecureWing => GameManagerHelpers.IsPlayerInSecureWing();
    bool IsPowerOn => GameManagerHelpers.GetPlayerPower() == PowerLevel.FullPower;
    
    private float powerBlend = 0f;      
    private float secureWingBlend = 0f; 

    void Update()
    {
        float targetPower = IsPowerOn ? 1f : 0f;
        powerBlend = Mathf.MoveTowards(powerBlend, targetPower, 1f/fadeTime * Time.deltaTime);
        
        lackOfPower.volume = Mathf.Cos(powerBlend * Mathf.PI * 0.5f) * maxVolume;
        power.volume       = Mathf.Sin(powerBlend * Mathf.PI * 0.5f) * maxVolume;
        
        float targetSecure = IsInSecureWing ? 1f : 0f;
        secureWingBlend = Mathf.MoveTowards(secureWingBlend, targetSecure, 1f/fadeTime * Time.deltaTime);
        secureWing.volume = Mathf.Sin(secureWingBlend * Mathf.PI * 0.5f) * maxVolume;
    }
}