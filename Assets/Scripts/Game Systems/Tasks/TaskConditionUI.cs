using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TaskConditionUI : PoweredDevice
    {
        [BoxGroup("References")][Required][SerializeField] TaskCondition taskCondition;
        
        [BoxGroup("References")][Required][SerializeField] TextMeshPro taskDescriptionText;
        
        void Update()
        {
            if (!IsPowered()){
                taskDescriptionText.text = "";
                return;
            }
            var task = taskCondition.task;
            if (task == null){
                return;
            }
            taskDescriptionText.text = $"{Descriptions.GetTaskDescription(task)} ({task.GetCompletion(GameStats.i.GetStats()) * 100f:0.#}%)";
        }
    }