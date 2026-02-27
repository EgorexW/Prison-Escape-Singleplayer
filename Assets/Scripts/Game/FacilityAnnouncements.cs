using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class FacilityAnnouncements : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PlayAudio audioPlayer;
    
    [SerializeField] float minTimeBetweenAnnouncements = 3;
    [SerializeField] Sound defaultSound;

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
        var sound = announcement.sound;
        if (sound == null){
            // Debug.Log("Announcement has no sound assigned", this);
            sound = defaultSound;
            if (sound == null){
                Debug.LogWarning("No default sound assigned for announcements", this);
                return;
            }
        }
        audioPlayer.sound = sound;
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