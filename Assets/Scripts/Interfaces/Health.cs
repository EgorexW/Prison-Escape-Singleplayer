using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

[Serializable]
[BoxGroup("Health")]
public struct Health
{
    [FormerlySerializedAs("health")] public float currentHealth;
    public float maxHealth;
    public float absoluteMaxHealth;
    public DamageType damagedBy;

    public bool Alive => currentHealth > 0;
    public Health(float currentHealth) : this(currentHealth, currentHealth, currentHealth) { }

    [FoldoutGroup("Events")] public UnityEvent<Damage> onDamage;

    public Health(float currentHealth, float maxHealth, float absoluteMaxHealth,
        DamageType damagedBy = DamageType.Physical)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        this.absoluteMaxHealth = absoluteMaxHealth;
        this.damagedBy = damagedBy;
        onDamage = new UnityEvent<Damage>();
    }

    public void Heal(Damage damage)
    {
        damage.Invert();
        Damage(damage, true);
    }

    public void Damage(Damage damage, bool ignoreImmunities = false)
    {
        if (!ignoreImmunities){
            if (damagedBy == 0){
                Debug.LogWarning("This entity is invulnerable");
                damage.SetZero();
            }
            // Debug.Log($"Damaged by {damage.damageType}, damageable by {damagedBy}");
            else if ((damagedBy & damage.damageType) == 0){
                damage.SetZero();
            }
        }
        onDamage.Invoke(damage);
        currentHealth -= damage.lightDamage;
        maxHealth -= damage.heavyDamage;
        maxHealth = Mathf.Clamp(maxHealth, 0, absoluteMaxHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Health))]
public class HealthDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var root = new VisualElement();

        var current = property.FindPropertyRelative("currentHealth");
        var max = property.FindPropertyRelative("maxHealth");
        var absMax = property.FindPropertyRelative("absoluteMaxHealth");

        // Create the foldout with the FloatField as the header
        var allHealthFoldout = new Foldout();
        allHealthFoldout.text = "Health";

// Create the FloatField
        var allHealthField = new FloatField("Quick Health"){ value = current.floatValue };
        allHealthField.RegisterValueChangedCallback(evt =>
        {
            current.floatValue = evt.newValue;
            max.floatValue = evt.newValue;
            absMax.floatValue = evt.newValue;
            property.serializedObject.ApplyModifiedProperties();
        });

// Add the FloatField as the foldout header
        root.Add(allHealthField);

// Add the three individual health fields inside the foldout
        allHealthFoldout.Add(new PropertyField(current));
        allHealthFoldout.Add(new PropertyField(max));
        allHealthFoldout.Add(new PropertyField(absMax));

// Add the foldout to the root
        root.Add(allHealthFoldout);

        // Draw default property fields
        root.Add(new PropertyField(property.FindPropertyRelative("damagedBy")));
        root.Add(new PropertyField(property.FindPropertyRelative("onDamage")));

        return root;
    }
}
#endif