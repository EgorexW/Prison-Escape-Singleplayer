using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class PowerHolderVisuals : MonoBehaviour
{
    [BoxGroup("References")][GetComponent][SerializeField] PowerHolder powerHolder;
    
    [BoxGroup("References")] [Required] [SerializeField] GameObject effectObject;
    [BoxGroup("References")] [Required] [SerializeField] ParticleSystem particles;

    void Awake()
    {
        powerHolder.onChargeChange.AddListener(OnChargeChange);
    }

    void Start()
    {
        OnChargeChange(powerHolder.IsCharged());
    }

    void OnChargeChange(bool charged)
    {
        effectObject.SetActive(charged);
        if (charged){
            if (!particles.isPlaying){
                particles.Play();
            }
        }
        else{
            particles.Stop();
        }
    }
}