using UnityEngine;

public class Bag : MonoBehaviour, IInteractive
{
    [SerializeField] int sizeIncrease = 1;

    public float HoldDuration{ get; } = 3;
    public void Interact(Player player)
    {
        player.GetInventory().IncreaseSize(sizeIncrease);
        Destroy(gameObject);
    }
}
