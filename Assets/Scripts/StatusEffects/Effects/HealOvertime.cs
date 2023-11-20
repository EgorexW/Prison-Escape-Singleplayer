using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealOvertime : IStatusEffect
{
    public Damage overAllHeal;
    public float totalTime;
    float startTime;

    public bool CanAddCopy(ICharacter character, IStatusEffect copy)
    {
        return true;
    }

    public void OnApply(ICharacter character)
    {
        startTime = Time.time;
    }

    public void OnRemove(ICharacter character)
    {
        
    }

    public void OnUpdate(ICharacter character)
    {
        character.Heal(overAllHeal * Time.deltaTime * (1/totalTime));
        if (Time.time - startTime >= totalTime){
            character.RemoveStatusEffect(this);
        }
    }
}
