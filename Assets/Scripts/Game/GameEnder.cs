using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameEnder : MonoBehaviour
{
    [SerializeField] string loseScene = "Lose Screen";
    
    [SerializeField] string winScene = "Win Screen";

    [FoldoutGroup("Events")] public UnityEvent beforeLoseGame;
    [FoldoutGroup("Events")] public UnityEvent beforeWinGame;
    [FoldoutGroup("Events")] public UnityEvent beforeEndGame;

    void Awake()
    {
        beforeLoseGame.AddListener(beforeEndGame.Invoke);
        beforeWinGame.AddListener(beforeEndGame.Invoke);
    }

    public void LoseGame()
    {
        beforeLoseGame.Invoke();
        SceneManager.LoadSceneAsync(loseScene);
    }

    public void WinGame()
    {
        beforeWinGame.Invoke();
        SceneManager.LoadSceneAsync(winScene);
    }
}