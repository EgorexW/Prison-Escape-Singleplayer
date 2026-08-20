using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class WarheadMusic : MonoBehaviour{
    const float TIME_END_ERROR = 1f;
    
    [BoxGroup("References")][Required][SerializeField] AudioSource warheadMusic;
    [BoxGroup("References")][Required][SerializeField] AmbienceMusic ambienceMusic;

    [SerializeField][Min(0.01f)] float fadeTime = 5f;
    [SerializeField] float maxVolume = 1f;

    float TimeLeft => GameManager.i.gameTimeManager.TimeLeft;
    float TrackLength => warheadMusic.clip.length;
    bool IsPlaying => warheadMusic.isPlaying;

    void Update(){
        if (TimeLeft < TIME_END_ERROR){
            return;
        }
        if (TimeLeft > TrackLength){
            if (!IsPlaying){
                return;
            }
            ambienceMusic.maxVolume = 1f;
            warheadMusic.Stop();
            return;    
        }
        if (!IsPlaying){
            warheadMusic.Play();
        }
        var playTime = TrackLength - TimeLeft;
        var volumeBlend = playTime / fadeTime;
        volumeBlend = Mathf.Clamp01(volumeBlend);
        
        UpdateVolume(volumeBlend);
    }

    void UpdateVolume(float volumeBlend){
        ambienceMusic.maxVolume = Mathf.Cos(volumeBlend * Mathf.PI * 0.5f) * maxVolume;
        warheadMusic.volume = Mathf.Sin(volumeBlend * Mathf.PI * 0.5f) * maxVolume;
    }
}
