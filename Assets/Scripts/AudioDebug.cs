using UnityEngine;

public class AudioDebug : MonoBehaviour
{
    public AudioClip testClip;

    void Update()
    {
        var source = gameObject.AddComponent<AudioSource>();
        source.clip = testClip;
        source.loop = true;
        source.playOnAwake = false;
        source.spatialBlend = 0; // 2D
        source.volume = 1f;
        source.Play();
        Debug.Log("Trying to play test clip");
    }
}