using Sirenix.OdinInspector;
using UnityEngine;

class PoisonTrap : MonoBehaviour, ITrap
{
    [BoxGroup("References")][Required][SerializeField] new BoxCollider collider;
    [SerializeField] ParticleSystem particles;
    
    [SerializeField] Damage damagePerSecond = new(2f);

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out var damagable)){
            damagable.Damage(damagePerSecond * Time.deltaTime);
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void SetRoom(Room room)
    {
        var bounds = room.roomNode.Bounds;
        collider.size = bounds.size;
        var particlesShape = particles.shape;
        particlesShape.scale = new Vector3(bounds.size.x, bounds.size.z, 1f);
        var particlesEmission = particles.emission;
        var particlesEmissionRateOverTime = particlesEmission.rateOverTime;
        particlesEmissionRateOverTime.constant = bounds.size.x * bounds.size.z * particlesEmission.rateOverTime.constant;
        particlesEmission.rateOverTime = particlesEmissionRateOverTime;
        transform.position = bounds.center;
        transform.rotation = room.roomNode.transform.rotation;
    }

    public bool Eligable(Room room)
    {
        return true;
    }
}