using UnityEngine;

[CreateAssetMenu(menuName ="StatusEffect/Paralized", fileName ="Paralized")]
public class ParalizedContainer : StatusEffectContainer
{   
    [SerializeField] float time;
    [SerializeField] AnimationCurve speedMod;

    public override IStatusEffect GetStatusEffect()
    {
        return new Paralized(time, speedMod);
    }
}
