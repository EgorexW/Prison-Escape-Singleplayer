using System;
using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

public class FireTrap : MonoBehaviour, IAIObject
{
    [SerializeField] [Required] GameObject effectPrefab;
    
    [SerializeField] 
    [PropertyRange(0.1f, 20f)]
    public Vector3 areaSize = new Vector3(5, 5, 5);

    [SerializeField] Damage damage;
    [SerializeField] AIObjectStats stats;
    [SerializeField] float noticedScore = 2;
    public AIObjectStats Stats => stats;
    
    List<MotionSensor> motionSensor;
    MainAI mainAI;

    public GameObject GameObject => gameObject;

    protected void Awake()
    {
        motionSensor = new List<MotionSensor>(GetComponentsInChildren<MotionSensor>());
        motionSensor.ForEach(sensor => sensor.onActivation.AddListener(Activate));
    }

    public void SetActive(bool active)
    {
        motionSensor.ForEach(sensor => sensor.SetActive(active));
    }
    public void Activate()
    {
        mainAI.PlayerNoticed(noticedScore);
        Collider[] objectsInArea = Physics.OverlapBox(transform.position, areaSize / 2);
        var damagablesHit = General.GetUniqueRootComponents<IDamagable>(objectsInArea);
        var effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        effect.transform.localScale = areaSize;
        foreach (IDamagable damagable in damagablesHit)
        {
            damagable.Damage(damage);
        }
        mainAI.RemoveObject(this);
        gameObject.SetActive(false);
    }

    public void Init(MainAI mainAI)
    {
        this.mainAI = mainAI;
    }
}
