using Sirenix.OdinInspector;
using UnityEngine;

public class GameTimeEffects : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] GameTime gameTime;

    [SerializeField] GameObject outOfTimeEffect;
    [SerializeField] FacilityAnnouncement announcement;

    void Awake()
    {
        gameTime.onOutOfTime.AddListener(OutOfTime);
        outOfTimeEffect.SetActive(false);
    }

    void OutOfTime()
    {
        GameDirector.i.facilityAnnouncements.AddAnnouncement(announcement);
        outOfTimeEffect.SetActive(true);
    }
}