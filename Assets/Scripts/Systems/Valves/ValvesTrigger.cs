using UnityEngine;

public class ValvesTrigger : MonoBehaviour
{
    [SerializeField] LootSpawner lootSpawner;
    
    public int triggerNumber = 5;
    
    bool activated;

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
    }
}
