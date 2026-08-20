using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class WarheadMusic : MonoBehaviour{
    const float TIME_END_ERROR = 1f;
    
    [BoxGroup("References")][Required][SerializeField] AudioSource warheadMusic;
    [BoxGroup("References")][Required][SerializeField] AmbienceMusic ambienceMusic;

    [FormerlySerializedAs("fadeTime")] [SerializeField][Min(0.01f)] float backgroundFadeTime = 5f;
    [FormerlySerializedAs("maxVolume")] [SerializeField][Range(0, 1)] float backgroundMaxVolume = 1f;
    [SerializeField] float startPlayingTime = 66f;

    float TimeLeft => GameManager.i.gameTimeManager.TimeLeft;
    bool IsPlaying => warheadMusic.isPlaying;

    void Update(){
        if (TimeLeft > startPlayingTime){
            if (!IsPlaying){
                return;
            }
            ambienceMusic.maxVolume = backgroundMaxVolume;
            warheadMusic.Stop();
            return;    
        }
        var playTime = startPlayingTime - TimeLeft;
        if (!IsPlaying && TimeLeft > 0f){
            warheadMusic.Play();
            warheadMusic.time = playTime;
        }
        float volumeBlend;
        if (TimeLeft > 0){
            volumeBlend = playTime / backgroundFadeTime;
            volumeBlend = Mathf.Clamp01(volumeBlend);
        }
        else{
            float trackTimeLeft = warheadMusic.clip.length - playTime;
            volumeBlend = trackTimeLeft / backgroundFadeTime;
            volumeBlend = Mathf.Clamp01(volumeBlend);
        }
        // Debug.Log($"WarheadMusic: playTime={playTime}, fadeTime={backgroundFadeTime}, volumeBlend={volumeBlend}");
        
        UpdateVolume(volumeBlend);
    }

    void UpdateVolume(float volumeBlend){
        ambienceMusic.maxVolume = Mathf.Cos(volumeBlend * Mathf.PI * 0.5f) * backgroundMaxVolume;
        // warheadMusic.volume = Mathf.Sin(volumeBlend * Mathf.PI * 0.5f) * maxVolume;
    }
}
