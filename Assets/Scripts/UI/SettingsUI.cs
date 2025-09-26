using System;
using Sirenix.OdinInspector;
using StarterAssets;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] FirstPersonController firstPersonController;

    void Awake()
    {
        firstPersonController.RotationSpeed = PlayerPrefs.GetFloat("MouseSensitivity", 1);
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        firstPersonController.RotationSpeed = sensitivity;
    }
}
