using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider maxHealthSlider;
    [SerializeField] TextMeshProUGUI text;

    public void SetHealth(Health health){
        SetHealth(health.health, health.maxHealth, health.absoluteMaxHealth);
    }
    public void SetHealth(float health, float maxHealth, float maxValue = -1){
        if (maxValue != -1){
            healthSlider.maxValue = maxValue;
            maxHealthSlider.maxValue = maxValue;
        }
        healthSlider.value = health;
        maxHealthSlider.value = maxHealth;
        text.text = Mathf.Round(health).ToString();
    }
}
