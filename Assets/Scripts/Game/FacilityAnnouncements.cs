using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class FacilityAnnouncements : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PlayAudio audioPlayer;

    [FoldoutGroup("Events")] public UnityEvent<FacilityAnnouncement> onAnnouncement;

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
        onAnnouncement.Invoke(announcement);
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