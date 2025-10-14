using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GameTimeEffects : MonoBehaviour
{
    [FormerlySerializedAs("gameTime")] [BoxGroup("References")] [Required] [SerializeField] GameTimeManager gameTimeManager;

    [SerializeField] GameObject outOfTimeEffect;
    [SerializeField] FacilityAnnouncement announcement;

    void Awake()
    {
        gameTimeManager.onOutOfTime.AddListener(OutOfTime);
        outOfTimeEffect.SetActive(false);
    }

    void OutOfTime()
    {
        GameManager.i.facilityAnnouncements.AddAnnouncement(announcement);
        outOfTimeEffect.SetActive(true);
    }
}