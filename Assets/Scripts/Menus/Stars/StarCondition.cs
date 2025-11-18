using System;
using UnityEngine;

public class StarCondition : MonoBehaviour
{
    [SerializeField] StarConditionType type;

    void Awake()
    {
        gameObject.SetActive(type switch
        {
            StarConditionType.Floor0Won => PlayerPrefs.GetInt(PlayerPrefsKeys.GamesWon, 0) > 0,
            StarConditionType.SecretCompleted05 => PlayerPrefs.GetInt(PlayerPrefsKeys.SecretCompleted05, 0) > 0,
            _ => false
        });
    }
}

enum StarConditionType
{
    Floor0Won,
    SecretCompleted05
}
