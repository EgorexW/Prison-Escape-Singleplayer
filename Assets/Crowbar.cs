using UnityEngine;

public class Crowbar : UseableItem
{
    [SerializeField] float breakStrength = 2;
    
    DoorLock doorLock;

    protected override void Apply()
    {
        doorLock.unlocked = true;
        doorLock.door.Open();
        player.RemoveItem(this);
        Destroy(gameObject);
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
            base.Use(playerTmp, false);
            return;
        }
        base.Use(playerTmp, true);
    }
}
