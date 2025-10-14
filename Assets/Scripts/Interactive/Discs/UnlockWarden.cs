using Sirenix.OdinInspector;
using UnityEngine;

public class UnlockWarden : FacilityTrigger
{
    [BoxGroup("References")] [Required] [SerializeField] KeycardReader wardenReader;
    [BoxGroup("References")] [Required] [SerializeField] AccessLevel newAccessLevel;

    [SerializeField] Damage newDamage;

    public override void Activate()
    {
        base.Activate();
        wardenReader.accessLevel = newAccessLevel;
        wardenReader.visuals?.UpdateAccessLevel();
        wardenReader.electrocutionDamage = newDamage;
        wardenReader.baseElectrocutionChance = 1;
    }
}