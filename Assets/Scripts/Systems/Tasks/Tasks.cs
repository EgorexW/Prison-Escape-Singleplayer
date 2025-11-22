using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Others/Tasks", fileName = "Tasks", order = 0)]
class Tasks : ScriptableObject
{
    [SerializeField] List<Task> tasks;
    
    public Task GetTask()
    {
        return tasks.Random();
    }
}

[Serializable]
public class Task
{
    public int normalDamageToTake;
    public int pernamentDamageToTake;
    public int uniqueRoomsToEnter;
    public int objectsToDestroy;
    [FormerlySerializedAs("uniqueItemsPickedUp")] public int uniqueItemsToPickUp;
}

public static class TaskExtensions
{
    public static bool IsCompleted(this Task task, Stats stats)
    {
        return task.GetCompletion(stats) >= 1f;
    }
    
    public static float GetCompletion(this Task task, Stats stats)
    {
        float sum = 0;
        float conditions = 0;
        
        if (task.normalDamageToTake > 0){
            conditions++;
            sum += Mathf.Clamp01(stats.normalDamageTaken / task.normalDamageToTake);
        }
        if (task.pernamentDamageToTake > 0){
            conditions++;
            sum += Mathf.Clamp01(stats.pernamentDamageTaken / task.pernamentDamageToTake);
        }
        if (task.uniqueRoomsToEnter > 0){
            conditions++;
            sum += Mathf.Clamp01((float)stats.uniqueRoomsEntered / task.uniqueRoomsToEnter);
        }
        if (task.objectsToDestroy > 0){
            conditions++;
            sum += Mathf.Clamp01((float)stats.objectsDestroyed / task.objectsToDestroy);
        }
        if (task.uniqueItemsToPickUp > 0){
            conditions++;
            sum += Mathf.Clamp01((float)stats.uniqueItemsPickedUp / task.uniqueItemsToPickUp);
        }

        float totalCompletion = sum / conditions;

        return totalCompletion;
    }
}