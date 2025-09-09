using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FireTrap : MonoBehaviour, ITrap
{
    [SerializeField] [GetComponent] BoxCollider boxCollider;
    [SerializeField] [Required] GameObject effectPrefab;

    public bool startActive = true;

    [SerializeField] Damage damage;

    List<MotionSensor> motionSensor;

    protected void Awake()
    {
        motionSensor = new List<MotionSensor>(GetComponentsInChildren<MotionSensor>());
        motionSensor.ForEach(sensor => sensor.onActivation.AddListener(Explode));
        SetActive(startActive);
        boxCollider.isTrigger = true;
    }

    public void Activate()
    {
        SetActive(true);
    }

    void SetActive(bool active)
    {
        motionSensor.ForEach(sensor => sensor.SetActive(active));
    }

    public void Explode()
    {
        var bounds = boxCollider.bounds;
        var objectsInArea = Physics.OverlapBox(bounds.center, bounds.extents);
        var damagablesHit = General.GetUniqueRootComponents<IDamagable>(objectsInArea);
        var effect = Instantiate(effectPrefab, bounds.center, Quaternion.identity);
        effect.transform.localScale = bounds.size;
        foreach (var damagable in damagablesHit) damagable.Damage(damage);
        gameObject.SetActive(false);
    }
}