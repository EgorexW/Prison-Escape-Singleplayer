using UnityEngine;

public abstract class StatusEffectContainer : ScriptableObject
{   
    public abstract IStatusEffect GetStatusEffect();
}
