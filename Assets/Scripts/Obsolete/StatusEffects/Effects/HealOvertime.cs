using System;
using UnityEngine;

[Serializable]
public class HealOvertime : IStatusEffect
{
    public Damage overAllHeal;
    public float totalTime;
    float startTime;

    public bool CanAddCopy(Player player, IStatusEffect copy)
    {
        return true;
    }

    public void OnApply(Player player)
    {
        startTime = Time.time;
    }

    public void OnRemove(Player player) { }

    public void OnUpdate(Player player)
    {
        player.Heal(overAllHeal * Time.deltaTime * (1 / totalTime));
        if (Time.time - startTime >= totalTime){
            player.RemoveStatusEffect(this);
        }
    }
}