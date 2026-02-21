using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class AlarmTrapVisuals : MonoBehaviour
{
    [BoxGroup("References")] [GetComponent] [SerializeField] AlarmTrap alarmTrap;

    [BoxGroup("References")] [Required] [SerializeField] Transform lights;
    [BoxGroup("References")][Required][SerializeField] PlayAudio alarmAudio;

    [SerializeField] float rotationSpeed = 360f;
            
    void Update()
    {
        var lockTime = alarmTrap.GetLockTime();
        lights.gameObject.SetActive(alarmTrap.IsActive());
        if (alarmTrap.IsActive()){
            lights.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            alarmAudio.Play();
        }
    }
}
