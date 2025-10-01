using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [BoxGroup("External References")][Required][SerializeField] FirstPersonController firstPersonController;
    
    [BoxGroup("Internal References")][Required][SerializeField] Slider sensitivitySlider;
    [BoxGroup("Internal References")][Required][SerializeField] TMP_Dropdown qualityDropdown;

    void Awake()
    {
        firstPersonController.RotationSpeed = PlayerPrefs.GetFloat("MouseSensitivity", 1);
        sensitivitySlider.value = firstPersonController.RotationSpeed;
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(SetQuality);
        var options = QualitySettings.names.Select(quality => new TMP_Dropdown.OptionData(quality)).ToList();
        qualityDropdown.options = options;
    }

    void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
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
