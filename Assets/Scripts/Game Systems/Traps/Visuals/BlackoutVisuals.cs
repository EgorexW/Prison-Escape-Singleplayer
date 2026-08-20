using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class BlackoutVisuals : MonoBehaviour
{
    [BoxGroup("References")] [GetComponent] [SerializeField] Blackout blackout;

    [BoxGroup("References")] [Required] [SerializeField] Transform lights;
    [BoxGroup("References")][Required][SerializeField] PlayAudio alarmAudio;

    [SerializeField] float rotationSpeed = 360f;

    bool active;

    void Awake()
    {
        blackout.onActivate.AddListener(OnActivate);
        blackout.onBlackout.AddListener(OnBlackout);
    }

    void OnBlackout()
    {
        active = false;
        lights.gameObject.SetActive(active);
        alarmAudio.Stop();
    }

    void OnActivate()
    {
        active = true;
        lights.gameObject.SetActive(active);
        alarmAudio.Play();
    }

    void Update()
    {
        if (!active){
            return;
        }
        lights.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
