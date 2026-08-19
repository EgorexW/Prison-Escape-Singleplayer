using System;
using System.Collections.Generic;
using NUnit.Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

public class AdaptiveMusic : MonoBehaviour
{
    const double START_DELAY = 0.3;

    [BoxGroup("References")] [SerializeField] AudioMixerGroup mixerGroup;

    [SerializeField][BoxGroup("Sounds")] AudioSource power;
    [SerializeField][BoxGroup("Sounds")] AudioSource lackOfPower;
    [SerializeField][BoxGroup("Sounds")] AudioSource secureWing;

    [SerializeField] [UnityEngine.Range(0f, 1f)] float masterVolume = 1f;
    [SerializeField] float defaultFadeSpeed = 0.5f;
    // [SerializeField] float updateFrequency = 2f;

    // float lastUpdateTime = 0f;
    
    float TimeLeft => GameManager.i.gameTimeManager.TimeLeft;
    bool IsInSecureWing => GameManagerHelpers.IsPlayerInSecureWing();
    bool IsPowerOn => GameManagerHelpers.GetPlayerPower() != PowerLevel.NoPower;
    

    void Update()
    {
        // if (Time.time - lastUpdateTime > updateFrequency){
        //     UpdateMusic();
        // }
        if (IsPowerOn){
            lackOfPower.volume = Mathf.Lerp(lackOfPower.volume, 0f, defaultFadeSpeed * Time.deltaTime);
            power.volume = Mathf.Lerp(power.volume, masterVolume, defaultFadeSpeed * Time.deltaTime);
        }
        else{
            power.volume = Mathf.Lerp(power.volume, 0f, defaultFadeSpeed * Time.deltaTime);
            lackOfPower.volume = Mathf.Lerp(lackOfPower.volume, masterVolume, defaultFadeSpeed * Time.deltaTime);
        }
        var secureVolume = IsInSecureWing ? masterVolume : 0f;
        secureWing.volume = Mathf.Lerp(secureWing.volume, secureVolume, defaultFadeSpeed * Time.deltaTime);
    }
}