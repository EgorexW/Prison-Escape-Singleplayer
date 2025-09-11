using UnityEngine;

public class Mask : UseableItem
{
    [SerializeField] DamageType protectionType;

    protected override void Apply()
    {
        player.playerHealth.AddProtection(protectionType);
        DestroyItem();
    }
}