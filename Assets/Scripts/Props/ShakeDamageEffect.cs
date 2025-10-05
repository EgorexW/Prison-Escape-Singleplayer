using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ShakeDamageEffect : MonoBehaviour
    {
        [BoxGroup("References")][Required][SerializeField] Destroyable connectedDestroyable;
        
        [BoxGroup("Settings")][SerializeField] float intensity = 0.2f;
        [BoxGroup("Settings")][SerializeField] float duration = 0.2f;
        [BoxGroup("Settings")][SerializeField] float chaos = 10;

        Vector3 defaultPosition;

        float wobbleEndTime;

        void Awake()
        {
            defaultPosition = transform.localPosition;
            connectedDestroyable.Health.onDamage.AddListener(OnDamaged);
        }

        void Update()
        {
            if (Time.time < wobbleEndTime){
                var targetPosition = defaultPosition + Random.insideUnitSphere * intensity;
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * chaos);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPosition, Time.deltaTime * chaos);
            }
        }

        void OnDamaged(Damage damage)
        {
            Shake();
        }
        
        [Button][HideInEditorMode]
        void Shake()
        {
            wobbleEndTime = Time.time + duration;
        }

        void Reset()
        {
            connectedDestroyable = GetComponentInChildren<Destroyable>();
        }
    }