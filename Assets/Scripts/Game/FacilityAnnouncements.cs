using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class FacilityAnnouncements : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PlayAudio audioPlayer;
    
    [SerializeField] float minTimeBetweenAnnouncements = 3;

    [FoldoutGroup("Events")] public UnityEvent<FacilityAnnouncement> onAnnouncement;

    readonly Queue<FacilityAnnouncement> announcements = new();

    float nextAnnouncementTime = -Mathf.Infinity;

    void Update()
    {
        if (Time.time < nextAnnouncementTime){
            return;
        }
        if (audioPlayer.IsPlaying){
            return;
        }
        if (announcements.Count <= 0){
            return;
        }
        var announcement = announcements.Dequeue();
        onAnnouncement.Invoke(announcement);
        nextAnnouncementTime = Time.time + minTimeBetweenAnnouncements;
        if (announcement.sound == null){
            Debug.Log("Announcement has no sound assigned", this);
            return;
        }
        audioPlayer.sound = announcement.sound;
        audioPlayer.Play();
    }

    public void AddAnnouncement(FacilityAnnouncement announcement)
    {
        announcements.Enqueue(announcement);
    }
}

[Serializable]
public struct FacilityAnnouncement
{
    public string message;
    public Sound sound;
}