using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class GameTime : MonoBehaviour
{
    [SerializeField] float gameTimeMinutes = 8f;
    [SerializeField] Damage outOfTimeDamage = new(2, 1);
    
    bool outOfTime = false;
    [ShowInInspector] public float TimeLeft => gameTimeMinutes * 60 - Time.timeSinceLevelLoad;

    [FoldoutGroup("Events")] public UnityEvent onOutOfTime;

    void Update()
    {
        if (outOfTime){
            GameDirector.i.Player.playerHealth.Damage(outOfTimeDamage * Time.deltaTime);
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
        gameTimeMinutes = Mathf.Max(gameTimeMinutes, 0);
    }
}