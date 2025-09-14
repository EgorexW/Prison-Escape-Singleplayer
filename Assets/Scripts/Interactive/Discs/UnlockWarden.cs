using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(DiscHandler))]
public class UnlockWarden : MonoBehaviour, IDiscHandler
{
    [BoxGroup("References")] [Required] [SerializeField] KeycardReader wardenReader;
    [BoxGroup("References")] [Required] [SerializeField] AccessLevel newAccessLevel;

    [SerializeField] Damage newDamage;

    public bool CanHandleDisc(Disc disc)
    {
        return disc.unlockWarden;
    }

    public void HandleDisc(Disc disc)
    {
        wardenReader.accessLevel = newAccessLevel;
        wardenReader.visuals?.UpdateAccessLevel();
        wardenReader.electrocutionDamage = newDamage;
        wardenReader.baseElectrocutionChance = 1;
    }
}