using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour
{
    [SerializeField] Sound sound;
    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        if (audioSource == null){
            audioSource = GetComponent<AudioSource>();
        }
    }
    void Reset(){
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Play(){
        if (audioSource.isPlaying && !sound.canOverride){
            return;
        }
        audioSource.loop = sound.loop;
		audioSource.outputAudioMixerGroup = sound.mixerGroup;
        audioSource.clip = sound.GetClip();
        audioSource.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
		audioSource.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));
        audioSource.Play();
    }
}
