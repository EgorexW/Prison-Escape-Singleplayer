using UnityEngine;

public class WinGameItem : MonoBehaviour, IInteractive
{
    public float HoldDuration => 3;
    
    public bool secret05;

    public void Interact(Player player)
    {
        if (secret05)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.SecretCompleted05, 1);
            // TODO add some feedback for secret found
        }
        GameManager.i.gameEnder.WinGame();
    }
}