using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class EmpGrenade : Grenade
{
    [InfoBox("Damage is ignored")]
    [SerializeField] float empStrenght = 20;

    protected override void ResolveHit(Transform hitTransform)
    {
        var device = General.GetRootComponent<IElectric>(hitTransform, false);
        device?.EmpHit(empStrenght);
    }
}