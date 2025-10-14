using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameItem : MonoBehaviour, IInteractive
{
    public float HoldDuration => 3;
    public void Interact(Player player)
    {
        GameManager.i.gameEnder.WinGame();
    }
}