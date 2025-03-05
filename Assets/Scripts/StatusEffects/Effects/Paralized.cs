using UnityEngine;

public class Paralized : IStatusEffect
{
    float time;
    AnimationCurve speedMod;
    float timePassed = 0;

    public Paralized(float time, AnimationCurve speedMod)
    {
        this.time = time;
        this.speedMod = speedMod;
    }

    public bool CanAddCopy(Character character, IStatusEffect copy)
    {
        character.RemoveStatusEffect(this);
        return true;
    }

    public void OnApply(Character character)
    {
        character.ModSpeed(speedMod.Evaluate(timePassed/time));
    }

    public void OnRemove(Character character)
    {
        character.ModSpeed(1/speedMod.Evaluate(timePassed-Time.deltaTime/time));
    }

    public void OnUpdate(Character character)
    {
        timePassed += Time.deltaTime;
        if (time <= timePassed){
            character.RemoveStatusEffect(this);
        }
        character.ModSpeed(1/speedMod.Evaluate(timePassed-Time.deltaTime/time));
        character.ModSpeed(speedMod.Evaluate(timePassed/time));
    }
}
