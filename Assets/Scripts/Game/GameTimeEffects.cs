using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GameTimeEffects : MonoBehaviour
{
    [FormerlySerializedAs("gameTime")] [BoxGroup("References")] [Required] [SerializeField]
    GameTimeManager gameTimeManager;

    [SerializeField] GameObject outOfTimeEffect;
    [SerializeField] FacilityAnnouncement announcement;
    [SerializeField] List<ObjectWithValue<FacilityAnnouncement>> timeLeftAnnouncements; 

    void Awake()
    {
        gameTimeManager.onOutOfTime.AddListener(OutOfTime);
        outOfTimeEffect.SetActive(false);
        foreach (var item in timeLeftAnnouncements){
            if (!(gameTimeManager.TimeLeft <= item.value)){
                continue;
            }
            GameManager.i.facilityAnnouncements.AddAnnouncement(item.Object);
            timeLeftAnnouncements.Remove(item);
            break;
        }
    }

    void OutOfTime()
    {
        GameManager.i.facilityAnnouncements.AddAnnouncement(announcement);
        outOfTimeEffect.SetActive(true);
    }
}