using UnityEngine;

public class Secret05 : MonoBehaviour, IInteractive
{
    public string triggerId;

    public float HoldDuration => 0;

    public void Interact(Player player)
    {
        var roundToInt = Mathf.RoundToInt(player.playerHealth.Health.currentHealth);
        if (roundToInt != 5){
            Debug.Log("Player health: " + roundToInt + ". Need exactly 5 to activate secret.");
            return;
        }
        Debug.Log("Secret activated!, health is exactly 5");
        GameManager.i.facilityTriggers.UnlockTriggers(triggerId);
    }
}