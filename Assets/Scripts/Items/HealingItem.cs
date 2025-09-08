using UnityEngine;

public class HealingItem : UseableItem
{
    [SerializeField] Damage heal;
    [SerializeField] Optional<HealOvertime> healOvertime;

    protected override void Apply()
    {
        player.Heal(heal);
        if (healOvertime){
            player.AddStatusEffect(healOvertime.Value);
        }
        player.RemoveItem(Item);
        Destroy(gameObject);
    }
}
