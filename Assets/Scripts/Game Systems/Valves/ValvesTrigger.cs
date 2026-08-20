using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ValvesTrigger : MonoBehaviour
{
    [SerializeField] LootSpawner lootSpawner;
    [SerializeField] Optional<FacilityAnnouncement> announcement;
    
    public int triggerNumber = 5;
    
    bool activated;

    [FoldoutGroup("Events")]
    public UnityEvent onActivate;

    public void Init()
    {
        GameManager.i.valvesManager.Register(this);       
    }

    public void Activate()
    {
        if (activated){
            return;
        }
        activated = true;
        lootSpawner?.Spawn();
        if (announcement){
            GameManager.i.facilityAnnouncements.AddAnnouncement(announcement);
        }
        onActivate.Invoke();
    }
}
