using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    [SerializeField] string loseScene = "Lose Screen";

    [SerializeField] string winScene = "Win Screen";

    [FoldoutGroup("Events")] public UnityEvent beforeLoseGame;
    [FoldoutGroup("Events")] public UnityEvent beforeWinGame;
    [FoldoutGroup("Events")] public UnityEvent beforeEndGame;

    bool endGameCalled;

    void Awake()
    {
        beforeLoseGame.AddListener(beforeEndGame.Invoke);
        beforeWinGame.AddListener(beforeEndGame.Invoke);
    }

    public void LoseGame()
    {
        if (endGameCalled){
            return;
        }
        endGameCalled = true;
        beforeLoseGame.Invoke();
        SceneManager.LoadSceneAsync(loseScene);
    }

    public void WinGame()
    {
        if (endGameCalled){
            return;
        }
        endGameCalled = true;
        beforeWinGame.Invoke();
        SceneManager.LoadSceneAsync(winScene);
    }
}