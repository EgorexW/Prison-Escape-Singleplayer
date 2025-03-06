using Sirenix.OdinInspector;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] Damage damage = 1;
    [SerializeField] float fireRate = 20;
    [SerializeField] int ammo = 20;
    
    [BoxGroup("Config")][SerializeField] LayerMask aimColliderLayerMask = new LayerMask();
    [BoxGroup("Config")][SerializeField] Transform vfxHitGreen;
    [BoxGroup("Config")][SerializeField] Transform vfxHitRed;

    float lastShotTime;

    public override void HoldUse(Character character, bool alternative = false){
        base.HoldUse(character);
        if (Time.time - lastShotTime < 1/fireRate){
            return;
        }
        if (ammo < 1){
            return;
        }
        ammo--;
        lastShotTime = Time.time;
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) {
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }
        if (hitTransform == null) return;
        var damagable = hitTransform.GetComponent<IDamagable>();
        Instantiate(damagable != null ? vfxHitGreen : vfxHitRed, mouseWorldPosition,
            Quaternion.identity);
        damagable?.Damage(damage);
    }
}
