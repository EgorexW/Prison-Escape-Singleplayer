using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GameTimeEffects : MonoBehaviour
{
    [FormerlySerializedAs("gameTime")] [BoxGroup("References")] [Required] [SerializeField]
    GameTimeManager gameTimeManager;

    [SerializeField] GameObject outOfTimeEffect;
    [SerializeField] Optional<FacilityAnnouncement> announcement;
    [SerializeField] List<TimeLeftAnnouncement> timeLeftAnnouncements; 

    void Awake()
    {
        gameTimeManager.onOutOfTime.AddListener(OutOfTime);
        outOfTimeEffect.SetActive(false);
    }

    void Update()
    {
        foreach (var item in timeLeftAnnouncements){
            if (gameTimeManager.TimeLeft > item.timeLeft){
                continue;
            }
            timeLeftAnnouncements.Remove(item);
            GameManager.i.facilityAnnouncements.AddAnnouncement(item.announcement);
            break;
        }
    }

    void OutOfTime()
    {
        if (announcement){
            GameManager.i.facilityAnnouncements.AddAnnouncement(announcement);
        }
        outOfTimeEffect.SetActive(true);
    }
}

[Serializable]
class TimeLeftAnnouncement
{
    public float timeLeft;
    public FacilityAnnouncement announcement;
}