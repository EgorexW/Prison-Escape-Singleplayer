using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] [Required] Slider healthSlider;
    [Required] [SerializeField] Slider maxHealthSlider;
    [Required] [SerializeField] TextMeshProUGUI text;

    [Required] [SerializeField] GameObject maskIcon;

    void Awake()
    {
        maskIcon.SetActive(false);
    }

    public void SetHealth(Health health)
    {
        SetHealth(health.currentHealth, health.maxHealth, health.absoluteMaxHealth);
    }

    public void SetHealth(float health, float maxHealth, float maxValue = -1)
    {
        if (maxValue > 0){
            healthSlider.maxValue = maxValue;
            maxHealthSlider.maxValue = maxValue;
        }
        healthSlider.value = health;
        maxHealthSlider.value = maxValue - maxHealth;
        text.text = Mathf.Round(health).ToString();
    }

    public void ShowDamage(Damage damage, Health health)
    {
        // TODO Hardcoded!
        maskIcon.SetActive(false);
        if (health.damagedBy.HasFlag(DamageType.Poison)){
            return;
        }
        if (damage.damageType != DamageType.Poison){
            return;
        }
        maskIcon.SetActive(true);
    }
}