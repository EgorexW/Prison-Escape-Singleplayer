using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class GameTimeManager : MonoBehaviour
{
    [SerializeField] public float gameTimeMinutes = 8f;
    [SerializeField] Damage outOfTimeDamage = new(2, 1);

    [FoldoutGroup("Events")] public UnityEvent onOutOfTime;

    bool outOfTime;
    float startTime = Mathf.Infinity;
    [ShowInInspector] public float GameTime => Time.time - startTime;
    [ShowInInspector] public float TimeLeft => gameTimeMinutes * 60 - GameTime;

    void Update()
    {
        if (outOfTime){
            GameManager.i.Player.playerHealth.Damage(outOfTimeDamage * Time.deltaTime);
            return;
        }
        if (TimeLeft <= 0){
            OutOfTime();
        }
    }

    void OutOfTime()
    {
        outOfTime = true;
        onOutOfTime.Invoke();
    }

    public void ChangeTime(float gameTimeIncrease)
    {
        gameTimeMinutes += gameTimeIncrease / 60f;
    }

    public void StartGame()
    {
        startTime = Time.time;
    }
}