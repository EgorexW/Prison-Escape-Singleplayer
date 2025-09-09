using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Door))]
public class DoorLock : PoweredDevice, IInteractive
{
    [GetComponent] [SerializeField] public Door door;

    public float resistance = 1;
    [SerializeField] bool requiresPower;

    [ShowIf("requiresPower")] [SerializeField] public bool requiresFullPower;

    public bool unlocked;

    public void Interact(Player player)
    {
        if (requiresPower){
            var power = GetPowerLevel();
            if (power == PowerLevel.NoPower){
                return;
            }
            if (requiresFullPower && power != PowerLevel.FullPower){
                return;
            }
        }
        if (unlocked){
            door.ChangeState();
        }
    }

    public float HoldDuration => 0;
}