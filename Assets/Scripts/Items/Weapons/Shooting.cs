using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{
    [SerializeField] bool log;

    public Damage damage = 1;
    public float fireRate = 20;
    public int ammo = 20;

    [BoxGroup("Config")] [SerializeField] LayerMask aimColliderLayerMask;

    [FormerlySerializedAs("notDamaged")] [FormerlySerializedAs("vfxHitGreen")] [BoxGroup("Config")] [SerializeField]
    Transform notDamagedEffect;

    [FormerlySerializedAs("vfxHitRed")] [BoxGroup("Config")] [SerializeField] Transform damagedEffect;

    [FoldoutGroup("Events")] public UnityEvent onShot;

    float lastShotTime;

    public void Shoot(Ray ray)
    {
        if (Time.time - lastShotTime < 1 / fireRate){
            return;
        }
        if (ammo < 1){
            return;
        }
        ammo--;
        lastShotTime = Time.time;
        Transform hitTransform = null;
        var hitPos = Vector3.zero;
        onShot?.Invoke();
        if (Physics.Raycast(ray, out var raycastHit, 999f, aimColliderLayerMask)){
            hitPos = raycastHit.point;
            hitTransform = raycastHit.transform;
        }
        if (hitTransform == null){
            return;
        }
        var damagable = hitTransform.GetComponent<IDamagable>();
        if (log){
            Debug.Log("Hit Object:" + hitTransform.gameObject.name, hitTransform.gameObject);
        }
        Instantiate(damagable != null ? damagedEffect : notDamagedEffect, hitPos, Quaternion.identity);
        damagable?.Damage(damage);
        if (log){
            Debug.Log("Dealt " + damage.damage + " damage to " + damagable, hitTransform.gameObject);
        }
    }
}