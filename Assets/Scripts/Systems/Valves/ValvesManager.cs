using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ValvesManager : MonoBehaviour
{
    [SerializeField] float valvesSpawnMult = 1.5f;
        
    [SerializeField][ReadOnly] List<Valve> valves = new List<Valve>();
    
    [SerializeField][ReadOnly] List<ValvesTrigger> triggers = new List<ValvesTrigger>();
    
    [SerializeField][ReadOnly] int activatedValves = 0;

    public void Register(ValvesTrigger trigger)
    {
        triggers.Add(trigger);
    }
    public void Register(Valve valve)
    {
        valves.Add(valve);
    }

    public void ValveActivated(Valve valve)
    {
        activatedValves++;
                foreach (var trigger in triggers){
                    if (activatedValves >= trigger.triggerNumber){
                        trigger.Activate();
                    }
                }
            
    }

    public void Setup()
    {
        int maxTriggerNr = 0;
        foreach (var trigger in triggers){
            maxTriggerNr = Mathf.Max(maxTriggerNr, trigger.triggerNumber);
        }
        int valvesToSpawn = Mathf.CeilToInt(maxTriggerNr * valvesSpawnMult);
        if (valvesToSpawn > valves.Count){
            Debug.LogError("Not enough valves in the scene! " + valvesToSpawn + " needed, but only " + valves.Count + " found.");
        }
        while (valves.Count > valvesToSpawn){
            var valve = valves.Random();
            valves.Remove(valve);
            Destroy(valve.gameObject);
        }
    }
}