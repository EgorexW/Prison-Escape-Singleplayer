using UnityEngine;

public class Bag : Equipment
{
    [SerializeField] int sizeIncrease = 1;

    protected override void Apply(Player player)
    {
        player.GetInventory().IncreaseSize(sizeIncrease);
    }
}