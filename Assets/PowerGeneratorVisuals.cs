using Sirenix.OdinInspector;
using UnityEngine;

public class PowerGeneratorVisuals : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] Light lightSource;
    [BoxGroup("References")][Required][SerializeField] ParticleSystem particles;
    
    void Start()
    {
        var generator = MainPowerSystem.i;
        generator.OnPowerChanged.AddListener(OnPowerChanged);
        OnPowerChanged();
    }

    void OnPowerChanged()
    {
        var powerLevel = MainPowerSystem.i.GetPower(transform.position);
        var fullPower = powerLevel == PowerLevel.FullPower;
        lightSource.enabled = !fullPower;
        if (!fullPower)
            particles.Play();
        else
            particles.Stop();
    }
}
