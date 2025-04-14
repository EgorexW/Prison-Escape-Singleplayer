using System;
using System.Collections.Generic;
using System.Linq;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(MainAI))]
public class AIObjects : MonoBehaviour
{
    [GetComponent] public MainAI mainAI;

    [SerializeField] float energy = 20;
    [SerializeField] int updateInterval = 120;
    [SerializeField] int minUpdateInterval = 20;
    [SerializeField] float energyIncreasePerUpdate;

    float lastUpdateTime = 0;

    void Awake()
    {
        lastUpdateTime = -minUpdateInterval;
    }

    void Update()
    {
        if (lastUpdateTime + updateInterval < Time.time){
            ResetObjects(mainAI.objects, mainAI.aiPlayerMarking.LastApproximatePos);
        }
    }

    public void ResetObjects(List<IAIObject> objects, PlayerApproximatePos playerApproximatePos)
    {
        if (lastUpdateTime + minUpdateInterval > Time.time){
            return;
        }
        lastUpdateTime = Time.time;
        foreach (var aiObject in objects) aiObject.SetActive(false);
        objects.Shuffle();
        var localEnergy = AllocateEnergy(objects, energy, true);
        if (localEnergy < 0){
            AllocateEnergy(objects, localEnergy);
        }
        energy += energyIncreasePerUpdate;
    }

    float AllocateEnergy(List<IAIObject> objects, float localEnergy, bool checkIfInRange = false)
    {
        foreach (var aiObject in objects.Where(aiObject => aiObject.Stats.energyCost <= localEnergy)){
            if (checkIfInRange){
                if (!CheckIfInErrorRange(aiObject, mainAI.aiPlayerMarking.LastApproximatePos)){
                    continue;
                }
            }
            aiObject.SetActive(true);
            Debug.DrawRay(aiObject.GameObject.transform.position, Vector3.up * 100, Color.red, 1);
            localEnergy -= aiObject.Stats.energyCost;
        }
        return localEnergy;
    }

    bool CheckIfInErrorRange(IAIObject aiObject, PlayerApproximatePos playerApproximatePos)
    {
        var pos = aiObject.GameObject.transform.position;
        return Vector3.Distance(pos, playerApproximatePos.pos) < playerApproximatePos.errorRadius;
    }
}