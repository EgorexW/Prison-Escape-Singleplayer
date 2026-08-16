using UnityEngine;

public class KeycardReactivater : Equipment
{
    void Awake()
    {
        destroyOnUse = false;
    }

    protected override void Apply(Player player)
    {
        var heldItem = player.GetHeldItem();
        if (heldItem == null){
            // Debug.Log("No item held, cannot upload keycard.");
            return;
        }
        if (!heldItem.TryGetComponent(out Keycard keycard)){
            // Debug.Log("Held item is not a keycard, cannot upload.");
            return;
        }
        if (keycard.Status != KeycardStatus.UseInactive){
            Debug.Log("Keycard is already active, cannot reactivate.");
            return;
        }
        destroyOnUse = true;
        ReactivateKeycard(keycard);
    }

    void ReactivateKeycard(Keycard keycard)
    {
        keycard.SetStatus(KeycardStatus.UseActive);
    }
}
