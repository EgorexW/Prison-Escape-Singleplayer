using UnityEngine;

public class HealingItem : UseableItem
{
    [SerializeField] Damage heal;
    [SerializeField] Optional<HealOvertime> healOvertime;

    protected override void Apply()
    {
        character.Heal(heal);
        if (healOvertime){
            character.AddStatusEffect(healOvertime.Value);
        }
        character.RemoveItem(this);
        Destroy(gameObject);
    }
}
