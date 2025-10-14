using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerHealthVisuals : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] PlayerHealth playerHealth;

    [BoxGroup("Particles")] [SerializeField] ParticleSystem hitParticles;

    [BoxGroup("Audio")] [SerializeField] PlayAudio playAudio;

    [BoxGroup("Other")] [SerializeField] CinemachineImpulseSource impulseSource;

    void Awake()
    {
        playerHealth.Health.onDamage.AddListener(OnDamage);
    }

    void OnDamage(Damage damage)
    {
        if (damage.IsDamage){
            return;
        }

        if (hitParticles != null){
            if (!hitParticles.isPlaying){
                hitParticles.Play();
            }
        }

        playAudio?.Play();

        impulseSource?.GenerateImpulse();
    }
}