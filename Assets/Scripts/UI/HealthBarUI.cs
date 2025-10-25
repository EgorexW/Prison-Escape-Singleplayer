using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] [Required] Slider healthSlider;
    [Required] [SerializeField] Slider maxHealthSlider;
    [Required] [SerializeField] TextMeshProUGUI text;

    [Required] [SerializeField] Image resistanceIcon;

    [SerializeField] List<ResistanceUI> resistanceIcons;

    [SerializeField] float showIconTime = 1;

    float lastResistanceSpriteTime;

    void Awake()
    {
        resistanceIcon.enabled = false;
    }

    void Update()
    {
        if (Time.time - lastResistanceSpriteTime > showIconTime){
            resistanceIcon.enabled = false;
        }
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
        if (health.damagedBy.HasFlag(damage.damageType)){
            return;
        }
        var icon = resistanceIcons.Find(ui => ui.damageType == damage.damageType);
        var resistanceIconEnabled = icon != null;
        if (!resistanceIconEnabled){
            return;
        }
        resistanceIcon.enabled = true;
        resistanceIcon.sprite = icon.sprite;
        resistanceIcon.color = icon.color;
        lastResistanceSpriteTime = Time.time;
    }
}

[Serializable]
class ResistanceUI
{
    public DamageType damageType;
    public Sprite sprite;
    public Color color;
}