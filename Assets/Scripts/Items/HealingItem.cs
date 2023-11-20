using System;
using System.Collections;
using System.Collections.Generic;
using FishNet;
using FishNet.Managing.Server;
using UnityEngine;

public class HealingItem : ItemBase
{
    [SerializeField] Damage heal;
    [SerializeField] Optional<HealOvertime> healOvertime;
    [SerializeField] float useTime;
    ICharacter character;
    float startUseTime = Mathf.Infinity;

    void Update(){
        if (Time.time - startUseTime < useTime){
            return;
        }
        Heal();
    }

    protected virtual void Heal()
    {
        character.Heal(heal);
        if (healOvertime){
            character.AddStatusEffect(healOvertime.Value);
        }
        character.RemoveItem(this);
        InstanceFinder.ServerManager.Despawn(gameObject);
        Destroy(gameObject);
    }

    public override void Use(ICharacter character, bool alternative = false)
    {
        if (!alternative){
            Use(character);
        } else {
            StopUse();
        }
        void Use(ICharacter character)
        {
            this.character = character;
            base.Use(character);
            startUseTime = Time.time;
        }
        void StopUse(){
            base.StopUse(character);
            startUseTime = Mathf.Infinity;
        }
    }
}
