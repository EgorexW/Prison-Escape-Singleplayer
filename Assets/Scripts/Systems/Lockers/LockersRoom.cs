using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class LockersRoom : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Room room;
    
    [BoxGroup("References")][Required][SerializeField] LockerRecipes lockerRecipes;
    
    [SerializeField] GameObject[] lockers;

    void Start()
    {
        Activate();
    }

    void Activate()
    {
        foreach (GameObject locker in lockers){
            ActivateLocker(locker);
        }
    }

    void ActivateLocker(GameObject locker)
    {
        var keycardReader = locker.GetComponentInChildren<KeycardReader>();
        var lootSpawner = locker.GetComponentInChildren<LootSpawner>();
        if (keycardReader == null || lootSpawner == null){
            Debug.LogError($"Locker {locker.name} is missing KeycardReader or LootSpawner component.", locker);
            return;
        }
        var choosenLockerType = ChooseLockerType();
        keycardReader.AccessLevel = choosenLockerType.accessLevel;
        keycardReader.StealKeycard = choosenLockerType.stealKeycard;
        lootSpawner.spawnTable = choosenLockerType.loot;
    }

    LockerRecipe ChooseLockerType()
    {
        return lockerRecipes.GetRandomRecipe();
    }
}

[Serializable]
class LockerRecipe
{
    public AccessLevel accessLevel;
    public bool stealKeycard;
    public SpawnTable loot;
}
