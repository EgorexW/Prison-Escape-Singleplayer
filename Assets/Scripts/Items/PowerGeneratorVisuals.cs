using Sirenix.OdinInspector;
using UnityEngine;

public class PowerGeneratorVisuals : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Light lightSource;
    [BoxGroup("References")][Required][SerializeField] ParticleSystem particles;
    
    void Update()
    {
        var powerLevel = MainPowerSystem.i.GetPower(transform.position);
        var fullPower = powerLevel == PowerLevel.FullPower;
        lightSource.enabled = !fullPower;
        if (!fullPower){
            if (!particles.isPlaying){
                particles.Play();
            }
        }
        else{
            
            particles.Stop();
        }
    }
}
