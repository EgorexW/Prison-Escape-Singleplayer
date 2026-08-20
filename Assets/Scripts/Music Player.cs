using Sirenix.OdinInspector;
using UnityEngine;

public class AmbienceMusic : MonoBehaviour
{
    [SerializeField][BoxGroup("Sounds")] AudioSource power;
    [SerializeField][BoxGroup("Sounds")] AudioSource lackOfPower;
    [SerializeField][BoxGroup("Sounds")] AudioSource secureWing;

    // [SerializeField] [UnityEngine.Range(0f, 1f)] float masterVolume = 1f;
    [SerializeField] float defaultFadeSpeed = 0.5f;
    
    float TimeLeft => GameManager.i.gameTimeManager.TimeLeft;
    bool IsInSecureWing => GameManagerHelpers.IsPlayerInSecureWing();
    bool IsPowerOn => GameManagerHelpers.GetPlayerPower() == PowerLevel.FullPower;
    
    private float powerBlend = 0f;      
    private float secureWingBlend = 0f; 

    void Update()
    {
        float targetPower = IsPowerOn ? 1f : 0f;
        powerBlend = Mathf.MoveTowards(powerBlend, targetPower, defaultFadeSpeed * Time.deltaTime);
        
        lackOfPower.volume = Mathf.Cos(powerBlend * Mathf.PI * 0.5f);
        power.volume       = Mathf.Sin(powerBlend * Mathf.PI * 0.5f);
        
        float targetSecure = IsInSecureWing ? 1f : 0f;
        secureWingBlend = Mathf.MoveTowards(secureWingBlend, targetSecure, defaultFadeSpeed * Time.deltaTime);
        secureWing.volume = Mathf.Sin(secureWingBlend * Mathf.PI * 0.5f);
    }
}