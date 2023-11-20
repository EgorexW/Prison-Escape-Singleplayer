using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffectContainer : ScriptableObject
{   
    public abstract IStatusEffect GetStatusEffect();
}
