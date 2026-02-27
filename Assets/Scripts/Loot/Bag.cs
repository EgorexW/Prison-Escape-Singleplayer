using UnityEngine;

public class Bag : Equipment
{
    [SerializeField] int sizeIncrease = 1;
    [SerializeField] float weightReduction = 0.5f;

    protected override void Apply(Player player)
    {
        player.GetInventory().IncreaseSize(sizeIncrease);
        player.GetInventory().weightReduction += weightReduction;
    }
}