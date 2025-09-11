using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]
public class FireTrap : PoweredDevice, ITrap
{
    [SerializeField] [GetComponent] BoxCollider boxCollider;
    [SerializeField] [Required] GameObject effectPrefab;

    [FormerlySerializedAs("startActive")] public bool active = true;

    [SerializeField] Damage damage;

    List<MotionSensor> motionSensor = new();

    protected override void Start()
    {
        base.Start();
        motionSensor = new List<MotionSensor>(GetComponentsInChildren<MotionSensor>());
        motionSensor.ForEach(sensor => sensor.onActivation.AddListener(Explode));
        SetActive();
        boxCollider.isTrigger = true;
    }

    public void Activate()
    {
        active = true;
        SetActive();
    }

    void SetActive()
    {
        motionSensor.RemoveAll(sensor => sensor == null);
        motionSensor.ForEach(sensor => sensor.SetActive(active && IsPowered()));
    }

    protected override void OnPowerChanged()
    {
        base.OnPowerChanged();
        SetActive();
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
    
    void Unlock()
    {
        active = false;
        SetActive();
    }
}