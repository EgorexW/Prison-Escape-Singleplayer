using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

class PoisonTrap : MonoBehaviour, ITrap
{
    [BoxGroup("References")][Required][SerializeField] new BoxCollider collider;
    [SerializeField] ParticleSystem particles;
    
    [SerializeField] Damage damagePerSecond = new(2f);
    
    LevelNode node;

    public LevelNode Node => node;

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

    public void SetRoom(Room room){
        SetNode(room.roomNode);
    }

    public void SetNode(LevelNode nodeTmp){
        node = nodeTmp;
        var bounds = node.Bounds;
        // Debug.Log($"Setting poison trap on node {node.name} with bounds {bounds.size}", this);
        collider.size = bounds.size;
        // Debug.Log($"Collider size set to {collider.size}", this);
        transform.position = bounds.center;
        transform.localRotation = node.transform.rotation;
        
        // Particles
        var particlesShape = particles.shape;
        particlesShape.scale = new Vector3(bounds.size.x, bounds.size.z, 1f);
        var particlesEmission = particles.emission;
        var particlesEmissionRateOverTime = particlesEmission.rateOverTime;
        particlesEmissionRateOverTime.constant = bounds.size.x * bounds.size.z * particlesEmission.rateOverTime.constant;
        particlesEmission.rateOverTime = particlesEmissionRateOverTime;
    }

    public bool Eligable(Room room)
    {
        return true;
    }
}