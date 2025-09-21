using UnityEngine;

public class Crowbar : EffectItem
{
    [SerializeField] float breakStrength = 2;

    DoorLock doorLock;

    protected override void Apply()
    {
        base.Apply();
        doorLock.unlocked = true;
        doorLock.door.Open();
        DestroyItem();
    }

    public override void Use(Player playerTmp, bool alternative = false)
    {
        if (!alternative){
            if (playerTmp.GetInteractive() is not DoorLock){
                return;
            }
            doorLock = playerTmp.GetInteractive() as DoorLock;
            if (doorLock.resistance > breakStrength){
                return;
            }
            base.Use(playerTmp);
            return;
        }
        base.Use(playerTmp, true);
    }
}