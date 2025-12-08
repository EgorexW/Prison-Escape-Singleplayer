using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class LootPortal : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] LootSpawner lootSpawner;
    
    [SerializeField] int totalLoot = 100;
    [SerializeField] float itemsPerSecond = 10f;

    int itemsSpawned = 0;
    float timeSinceLastSpawn = 0f;
    
    void Update()
    {
        if (itemsSpawned >=totalLoot){
            return;
        }
        timeSinceLastSpawn += Time.deltaTime;
        float secondsPerItem = 1f / itemsPerSecond;
        while (timeSinceLastSpawn >= secondsPerItem && itemsSpawned < totalLoot){
            lootSpawner.Spawn();
            itemsSpawned++;
            timeSinceLastSpawn -= secondsPerItem;
        }  
    }
}
