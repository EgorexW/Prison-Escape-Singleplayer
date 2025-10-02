using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameEnder : MonoBehaviour
{
    [SerializeField] string loseScene = "Lose Screen";
    
    [SerializeField] string winScene = "Win Screen";
    [SerializeField] int winDelay = 2;

    [FoldoutGroup("Events")] public UnityEvent beforeLoseGame;
    [FoldoutGroup("Events")] public UnityEvent beforeWinGame;
    
    public void LoseGame()
    {
        beforeLoseGame.Invoke();
        SceneManager.LoadSceneAsync(loseScene);
    }

    public void WinGame()
    {
        beforeWinGame.Invoke();
        General.CallAfterSeconds(() => SceneManager.LoadSceneAsync(winScene), winDelay);
    }
}