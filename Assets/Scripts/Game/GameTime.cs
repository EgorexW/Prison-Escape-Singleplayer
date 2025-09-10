using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] float gameTimeMinutes = 8f;
    [SerializeField] Damage outOfTimeDamage = new Damage(2, 1);
    public float TimeLeft => gameTimeMinutes * 60 - Time.timeSinceLevelLoad;

    void Update()
    {
        if (TimeLeft <= 0){
            OutOfTime();
        }
    }

    void OutOfTime()
    {
        GameDirector.i.Player.Damage(outOfTimeDamage * Time.deltaTime);
    }
}