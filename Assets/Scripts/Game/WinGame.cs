using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    public void Win()
    {
        GameManager.i.gameEnder.WinGame();
    }
}