using Sirenix.OdinInspector;
using UnityEngine;

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