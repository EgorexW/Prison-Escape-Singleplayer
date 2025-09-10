using Sirenix.OdinInspector;
using UnityEngine;

public class GameTimeEffects : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] GameTime gameTime;
    
    [SerializeField] GameObject outOfTimeEffect;
    
    void Update()
    {
        outOfTimeEffect.SetActive(gameTime.TimeLeft <= 0);
    }
}