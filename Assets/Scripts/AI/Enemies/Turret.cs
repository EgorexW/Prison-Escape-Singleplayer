using UnityEngine;

[RequireComponent(typeof(Shooting))]
[RequireComponent(typeof(TargetsSeeing))]
public class Turret : TurretBase, IDamagable, IElectric, IAIObject
{
    const float UNDERGROUND_HIDE_OFFSET = 3;
    const float HIDE_TIME = 1f;
    
    [SerializeField] GameObject workingLight;

    [SerializeField] Health health;
    [SerializeField] float empResistance = 1;
    [SerializeField] float fireRateLossPerDmg = 0.08f;
    [SerializeField] AIObjectStats stats;
    
    MainAI mainAI;
    PlayerMark playerMark;
    float startPosY;

    protected override void Awake()
    {
        base.Awake();
        startPosY = transform.position.y;
        playerMark = PlayerMark.Inactive;
    }

    public GameObject GameObject => gameObject;

    public AIObjectStats Stats => stats;


    public void SetActive(bool active)
    {
        LeanTween.cancel(gameObject);
        if (active){
            transform.LeanMoveY(startPosY, HIDE_TIME);
        }
        else{
            transform.LeanMoveY(startPosY - UNDERGROUND_HIDE_OFFSET, HIDE_TIME);
        }
        workingLight?.SetActive(active);
        enabled = active;
    }

    public void Init(MainAI mainAI)
    {
        this.mainAI = mainAI;
        targets = mainAI.Targets;
        mainAI.aiPlayerMarking.onPlayerApproximatePosChanged.AddListener(OnPlayerPosChanged);
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

    void OnPlayerPosChanged(PlayerApproximatePos pos)
    {
        defaultRotation = Quaternion.LookRotation(pos.pos - transform.position);
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