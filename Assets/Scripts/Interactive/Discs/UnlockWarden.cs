using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(DiscHandler))]
public class UnlockWarden : MonoBehaviour, IDiskHandler
{
    [BoxGroup("References")] [Required] [SerializeField] KeycardReader wardenReader;
    [BoxGroup("References")] [Required] [SerializeField] AccessLevel newAccessLevel;

    [SerializeField] Damage newDamage;

    public bool CanHandleDisk(Disc disc)
    {
        return disc.unlockWarden;
    }

    public void HandleDisk(Disc disc)
    {
        wardenReader.accessLevel = newAccessLevel;
        wardenReader.electrocutionDamage = newDamage;
        wardenReader.baseElectrocutionChance = 1;
    }
}