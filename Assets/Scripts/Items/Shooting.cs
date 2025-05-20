using Sirenix.OdinInspector;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] bool log;
    
    public Damage damage = 1;
    public float fireRate = 20;
    public int ammo = 20;
    public Noise noisePerShot = new (1);
    
    [BoxGroup("Config")][SerializeField] LayerMask aimColliderLayerMask;
    [BoxGroup("Config")][SerializeField] Transform vfxHitGreen;
    [BoxGroup("Config")][SerializeField] Transform vfxHitRed;
    
    float lastShotTime;

    public void Shoot(Ray ray)
    {
        if (Time.time - lastShotTime < 1/fireRate){
            return;
        }
        if (ammo < 1){
            return;
        }
        ammo--;
        lastShotTime = Time.time;
        noisePerShot.source = gameObject;
        noisePerShot.pos = transform.position;
        General.GetRootComponent<INoiseReciver>(transform, false)?.ReceiveNoise(noisePerShot);
        Transform hitTransform = null;
        var hitPos = Vector3.zero;
        if (Physics.Raycast(ray, out var raycastHit, 999f, aimColliderLayerMask)) {
            hitPos = raycastHit.point;
            hitTransform = raycastHit.transform;
        }
        if (hitTransform == null) return;
        var damagable = General.GetRootComponent<IDamagable>(hitTransform, false);
        if (log) Debug.Log("Hit Object:" + hitTransform.gameObject.name, hitTransform.gameObject);
        Instantiate(damagable != null ? vfxHitGreen : vfxHitRed, hitPos, Quaternion.identity);
        damagable?.Damage(damage);
    }
}