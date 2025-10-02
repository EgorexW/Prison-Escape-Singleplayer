using UnityEngine;

public class Crowbar : EffectItem
{
    [SerializeField] float breakStrength = 2;

    DoorLock doorLock;

    protected override void Apply()
    {
        base.Apply();
        doorLock.Unlock();
        DestroyItem();
    }

    public override void Use(Player playerTmp, bool alternative = false)
    {
        if (playerTmp.GetInteractive() is not DoorLock){
            return;
        }
        doorLock = playerTmp.GetInteractive() as DoorLock;
        Debug.Assert(doorLock != null, nameof(doorLock) + " != null");
        if (doorLock.resistance > breakStrength){
            return;
        }
        base.Use(playerTmp);
    }
}