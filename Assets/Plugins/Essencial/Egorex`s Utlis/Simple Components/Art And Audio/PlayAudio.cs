using Sirenix.OdinInspector;
using UnityEngine;

public sealed class PlayAudio : MonoBehaviour
{
    [Required][SerializeField] Sound sound;
    
    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        if (audioSource == null){
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Reset(){
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null){
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    [Button]
    public void Play(){
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
