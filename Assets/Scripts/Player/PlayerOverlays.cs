using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerOverlays : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] GameObject map;
    [BoxGroup("References")][Required][SerializeField] KeycardDetectorOverlay keycardDetector;
    [BoxGroup("References")] [Required] [SerializeField] Player player;
    
    PlayerOverlay activeOverlay = PlayerOverlay.None;
    
    public void SelectOverlay(PlayerOverlay playerOverlay)
    {
        DeselectAll();
        activeOverlay = playerOverlay;
        switch (playerOverlay){
            case PlayerOverlay.Map:
                map.SetActive(true);
                break;
            case PlayerOverlay.KeycardDetector:
                keycardDetector.Init(player);
                break;
            default:
                Debug.LogWarning($"PlayerOverlay {playerOverlay} not handled in SelectOverlay.", this);
                break;
        }
    }

    void DeselectAll()
    {
        map.SetActive(false);
        keycardDetector.Hide();
    }

    public void DeselectOverlay(PlayerOverlay playerOverlay)
    {
        if (playerOverlay != activeOverlay){
            return;
        }
        DeselectAll();
        activeOverlay = PlayerOverlay.None;
    }
}

public enum PlayerOverlay
{
    None,
    Map,
    KeycardDetector
}