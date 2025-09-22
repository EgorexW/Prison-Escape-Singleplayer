using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FacilityAnnouncements : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PlayAudio audioPlayer;

    readonly Queue<FacilityAnnouncement> announcements = new();

    void Update()
    {
        if (audioPlayer.IsPlaying){
            return;
        }
        if (announcements.Count <= 0){
            return;
        }
        var announcement = announcements.Dequeue();
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
    public Sound sound;
    // public string message;
}