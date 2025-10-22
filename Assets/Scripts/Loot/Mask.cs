using UnityEngine;

public class Mask : Equipment
{
    [SerializeField] DamageType protectionType;

    protected override void Apply(Player player)
    {
        player.playerHealth.AddProtection(protectionType);
    }
}