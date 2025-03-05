using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerPlugin : MonoBehaviour
{
    [SerializeField] string soundName;

    public void Play(){
        AudioManager.i.Play(soundName);
    }
}
