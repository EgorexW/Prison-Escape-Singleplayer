using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider maxHealthSlider;
    [SerializeField] TextMeshProUGUI text;

    public void SetHealth(Health health){
        SetHealth(health.currentHealth, health.maxHealth, health.absoluteMaxHealth);
    }
    public void SetHealth(float health, float maxHealth, float maxValue = -1){
        if (maxValue > 0){
            healthSlider.maxValue = maxValue;
            maxHealthSlider.maxValue = maxValue;
        }
        healthSlider.value = health;
        maxHealthSlider.value = maxValue - maxHealth;
        text.text = Mathf.Round(health).ToString();
    }
}
