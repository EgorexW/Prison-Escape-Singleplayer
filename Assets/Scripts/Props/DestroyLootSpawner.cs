using System;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;


[RequireComponent(typeof(LootSpawner))]
public class DestroyLootSpawner : MonoBehaviour
{
    [GetComponent] [SerializeField] LootSpawner lootSpawner;
        [BoxGroup("References")][Required][SerializeField] Destroyable destroyable;

        void Start()
        {
            destroyable.Health.onDamage.AddListener(OnDamage);
            transform.parent = destroyable.GameObjectToDestroy.transform.parent;
            transform.localScale = Vector3.one;
        }

        void OnDamage(Damage arg0)
        {
            if (!destroyable.Health.Alive)
            {
                lootSpawner.SpawnGameObjects();
            }
        }

        void OnValidate()
        {
            if (lootSpawner != null){
                lootSpawner.spawnOnAwake = false;
            }
        }
    }