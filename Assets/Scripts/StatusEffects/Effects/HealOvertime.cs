using System;
using UnityEngine;

[Serializable]
public class HealOvertime : IStatusEffect
{
    public Damage overAllHeal;
    public float totalTime;
    float startTime;

    public bool CanAddCopy(Character character, IStatusEffect copy)
    {
        return true;
    }

    public void OnApply(Character character)
    {
        startTime = Time.time;
    }

    public void OnRemove(Character character)
    {
        
    }

    public void OnUpdate(Character character)
    {
        character.Heal(overAllHeal * Time.deltaTime * (1/totalTime));
        if (Time.time - startTime >= totalTime){
            character.RemoveStatusEffect(this);
        }
    }
}
