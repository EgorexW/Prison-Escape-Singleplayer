using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Shooting))]
[RequireComponent(typeof(TargetsSeeing))]
public class Turret : TurretBase, IDamagable, IElectric, IAIObject
{
    [SerializeField] Health health;
    [SerializeField] float empResistance = 1;

    [SerializeField] float fireRateLossPerDmg = 0.08f;

    public float EmpResistance => empResistance;
    public Health Health => health;
    public AIObjectType aiType => AIObjectType.Turret;
    
    MainAI mainAI;
    PlayerMark playerMark;

    protected override void Awake()
    {
        base.Awake();
        playerMark = PlayerMark.Inactive;
    }
    public void Damage(Damage damage)
    {
        health.Damage(damage);
        shooting.fireRate -= damage.damage * fireRateLossPerDmg;
        if (!health.Alive){
            gameObject.SetActive(false);
        }
    }

    protected override void StartAiming(GameObject target)
    {
        if (playerMark.Active){
            playerMark.time = Time.time;
            playerMark.position = target.transform.position;
        }
        else{
            playerMark = new PlayerMark(target.transform.position);
            mainAI.PlayerNoticed(playerMark);
        }
        base.StartAiming(target);
    }
    public void Init(MainAI mainAI)
    {
        this.mainAI = mainAI;
        targets = mainAI.Targets;
    }


    public void EmpHit(float strenght)
    {
        enabled = false;
        General.CallAfterSeconds(() => enabled = true, strenght/empResistance);
    }
}