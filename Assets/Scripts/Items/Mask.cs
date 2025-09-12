using UnityEngine;

public class Mask : MonoBehaviour, IInteractive
{
    [SerializeField] DamageType protectionType;
    
    public float HoldDuration{ get; } = 5;
    public void Interact(Player player)
    {
        player.playerHealth.AddProtection(protectionType);
        Destroy(gameObject);
    }
}