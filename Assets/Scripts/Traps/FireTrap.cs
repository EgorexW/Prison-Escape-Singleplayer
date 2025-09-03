using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] [Required] GameObject effectPrefab;
    
    [SerializeField] 
    public Vector3 areaSize = new Vector3(5, 5, 5);

    [SerializeField] Damage damage;
    
    List<MotionSensor> motionSensor;

    protected void Awake()
    {
        motionSensor = new List<MotionSensor>(GetComponentsInChildren<MotionSensor>());
        motionSensor.ForEach(sensor => sensor.onActivation.AddListener(Activate));
    }
    
    public void Activate()
    {
        var objectsInArea = Physics.OverlapBox(transform.position, areaSize / 2);
        var damagablesHit = General.GetUniqueRootComponents<IDamagable>(objectsInArea);
        var effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        effect.transform.localScale = areaSize;
        foreach (var damagable in damagablesHit)
        {
            damagable.Damage(damage);
        }
        gameObject.SetActive(false);
    }
}
