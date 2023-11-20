using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AudioManager : MonoBehaviour
{

	public static AudioManager i;

	public AudioMixerGroup mixerGroup;

	[Expandable]
	public Sound[] sounds;
	List<AudioSource> audioSources = new List<AudioSource>();

	void Awake()
	{
		if (i != null)
		{
			Destroy(gameObject);
		}
		else
		{
			i = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			AudioSource source = gameObject.AddComponent<AudioSource>();
			audioSources.Add(source);
			source.loop = s.loop;

			source.outputAudioMixerGroup = mixerGroup;
		}
	}

	public void Play(string sound)
	{
		int nr = Array.FindIndex(sounds, item => item.name == sound);
		if (nr == -1)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		Sound s = sounds[nr];
		AudioSource source = audioSources[nr];
		if (source.isPlaying && !s.canOverride){
			return;
		}
		source.clip = s.GetClip();
		source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		source.Play();
	}
	public void Stop(string sound)
	{
		int nr = Array.FindIndex(sounds, item => item.name == sound);
		if (nr == -1)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		Sound s = sounds[nr];
		AudioSource source = audioSources[nr];
		source.Stop();
	}

}
