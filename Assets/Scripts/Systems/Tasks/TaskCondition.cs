using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class TaskCondition : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Tasks tasks;
    [BoxGroup("References")][Required][SerializeField] Room room;
    
    public Task task;
    
    bool completed;

    void Start()
    {
        task = tasks.GetTask();
    }

    void Update()
    {
        if (completed){
            return;
        }
        if (!task.IsCompleted(GameStats.i.GetStats())){
            return;
        }
        completed = true;
        room.doorway.GetDoor().Open();
    }
}
