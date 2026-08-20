using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LockersRoom : MonoBehaviour
{
    // [BoxGroup("References")] [Required] [SerializeField] Room room;
    
    [BoxGroup("References")][Required][SerializeField] LockerRecipes lockerRecipes;
    
    [SerializeField] GameObject[] lockers;

    void Start()
    {
        Activate();
    }

    void Activate()
    {
        var recipes = lockerRecipes.GetRandomRecipes(lockers.Length);
        for (int i = 0; i < lockers.Length; i++)
        {
            ActivateLocker(lockers[i], recipes[i]);
        }
    }

    void ActivateLocker(GameObject locker, LockerRecipe recipe)
    {
        var keycardReader = locker.GetComponentInChildren<KeycardReader>();
        var lootSpawner = locker.GetComponentInChildren<LootSpawner>();
        if (keycardReader == null || lootSpawner == null){
            Debug.LogError($"Locker {locker.name} is missing KeycardReader or LootSpawner component.", locker);
            return;
        }
        keycardReader.AccessLevel = recipe.accessLevel;
        keycardReader.StealKeycard = recipe.stealKeycard;
        lootSpawner.spawnTable = recipe.loot;
    }
}

[Serializable]
class LockerRecipe
{
    public AccessLevel accessLevel;
    public bool stealKeycard;
    public SpawnTable loot;
}
