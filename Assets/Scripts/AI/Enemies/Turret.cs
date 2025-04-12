using UnityEngine;

[RequireComponent(typeof(Shooting))]
[RequireComponent(typeof(TargetsSeeing))]
public class Turret : TurretBase, IDamagable, IElectric, IAIObject
{
    [SerializeField] GameObject workingLight; 
    
    [SerializeField] Health health;
    [SerializeField] float empResistance = 1;
    [SerializeField] float fireRateLossPerDmg = 0.08f;
    [SerializeField] AIObjectStats stats;
    
    
    MainAI mainAI;
    PlayerMark playerMark;
    
    public GameObject GameObject => gameObject;

    protected override void Awake()
    {
        base.Awake();
        playerMark = PlayerMark.Inactive;
    }

    public AIObjectStats Stats => stats;


    public void SetActive(bool active)
    {
        workingLight?.SetActive(active);
        enabled = active;
    }

    public void Init(MainAI mainAI)
    {
        this.mainAI = mainAI;
        targets = mainAI.Targets;
    }

    public Health Health => health;

    public void Damage(Damage damage)
    {
        health.Damage(damage);
        shooting.fireRate -= damage.damage * fireRateLossPerDmg;
        if (health.Alive){
            return;
        }
        mainAI.RemoveObject(this);
        gameObject.SetActive(false);
    }

    public float EmpResistance => empResistance;


    public void EmpHit(float strenght)
    {
        enabled = false;
        General.CallAfterSeconds(() => enabled = true, strenght / empResistance);
    }

    protected override void StartAiming(GameObject target)
    {
        if (playerMark.Active){
            playerMark.time = Time.time;
            playerMark.position = target.transform.position;
        }
        else{
            playerMark = new PlayerMark(target.transform.position);
            mainAI.aiPlayerMarking.PlayerNoticed(playerMark);
        }
        base.StartAiming(target);
    }
}