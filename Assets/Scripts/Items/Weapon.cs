using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ItemBase
{
    [SerializeField] Damage damage = 1;
    [SerializeField] StatusEffectContainer[] statusEffectsApplied;
    [SerializeField] float range = 100;
    [SerializeField] float fireRate = 20; 

    float lastShotTime;

    public override void HoldUse(ICharacter character, bool alternative = false){
        base.HoldUse(character);
        if (Time.time - lastShotTime < 1/fireRate){
            return;
        }
        lastShotTime = Time.time;
        RaycastHit[] raycasts = Physics.RaycastAll(character.GetAimTransform().position, character.GetAimTransform().forward, range);
        Debug.DrawRay(character.GetAimTransform().position, character.GetAimTransform().forward * 10, Color.red, 1f);
        Debug.Log("Raycast count " + raycasts.Length);
        foreach (RaycastHit raycast in raycasts)
        {
            Debug.Log("Hit", raycast.collider);
            if (!raycast.collider.TryGetComponent(out IDamagable damagable)){
                continue;
            }
            Debug.Log("Hit IDamagable", raycast.collider);
            if (damagable == character){
                continue;
            }
            Debug.Log("Damaged IDamagable", raycast.collider);
            damagable.Damage(damage);
            if (damagable is not ICharacter){
                return;
            }
            ICharacter hitCharacter = (ICharacter)damagable;
            foreach (StatusEffectContainer statusEffectApplied in statusEffectsApplied)
            {
                hitCharacter.AddStatusEffect(statusEffectApplied.GetStatusEffect());
            }               
            break;
        }
    }
}
