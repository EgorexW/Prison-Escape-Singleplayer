using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerStats i;

    void Awake()
    {
        if (i == null){
            i = this;
        }
        else{
            Debug.LogWarning("Multiple instances of PlayerStats detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        OnStartGame();
        GameManager.i.gameEnder.beforeLoseGame.AddListener(BeforeLoseGame);
        GameManager.i.gameEnder.beforeWinGame.AddListener(BeforeWinGame);
    }

    void BeforeWinGame()
    {
        AddToPlayerPref("Games Won", 1);
        Debug.Log("Games Won: " + PlayerPrefs.GetInt("Games Won", 0));
    }

    void BeforeLoseGame()
    {
        AddToPlayerPref("Games Lost", 1);
        Debug.Log("Games Lost: " + PlayerPrefs.GetInt("Games Lost", 0));
    }

    void OnStartGame()
    {
        AddToPlayerPref("Games Started", 1);
        Debug.Log("Games Started: " + PlayerPrefs.GetInt("Games Started", 0));
    }

    public void AddToPlayerPref(string key, int value, int defaultValue = 0)
    {
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key, defaultValue) + value);
    }
}
