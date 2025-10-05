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

    public void SetHealth(Health health)
    {
        SetHealth(health.currentHealth, health.maxHealth, health.absoluteMaxHealth);

        maskIcon.SetActive(!health.damagedBy.HasFlag(DamageType.Poison));
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
}