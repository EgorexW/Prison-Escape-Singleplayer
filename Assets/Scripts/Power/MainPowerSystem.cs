using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MainPowerSystem : MonoBehaviour, IPowerSource
{
    [SerializeField] List<PowerLevel> startPowerDistribution;
    [SerializeField] List<SubPowerSystem> subPowerSystems;
    [SerializeField] float timeBetweenPowerLoss = 90f;

    [ShowInInspector] [BoxGroup("Debug")] float lastPowerLossTime;
    public static MainPowerSystem i{ get; private set; }

    public List<SubPowerSystem> SubPowerSystems => subPowerSystems.Copy();
    [ShowInInspector] [BoxGroup("Debug")] public bool GlobalMinimalPower{ get; private set; }

    [FoldoutGroup("Events")]
    public UnityEvent onPowerChanged = new();
    [FoldoutGroup("Events")]
    public UnityEvent onMinimalPowerChanged = new();

    public UnityEvent OnPowerChanged => onPowerChanged;
    public UnityEvent OnMinimalPowerChanged => onMinimalPowerChanged;
    
    void Awake()
    {
        if (i != null && i != this){
            Debug.LogWarning("There was another instance", this);
            Destroy(gameObject);
            return;
        }
        i = this;
    }

    void Start()
    {
        lastPowerLossTime = Time.time;
        startPowerDistribution.Shuffle();
        for (var i = 0; i < subPowerSystems.Count; i++)
            ChangePower(subPowerSystems[i], startPowerDistribution[i % startPowerDistribution.Count]);
    }

    void Update()
    {
        if (Time.time - lastPowerLossTime > timeBetweenPowerLoss){
            LosePower();
        }
    }

    public PowerLevel GetPower(Vector3 pos)
    {
        foreach (var subSystem in subPowerSystems)
            if (subSystem.InBounds(pos)){
                var subSystemPower = subSystem.power;
                if (GlobalMinimalPower && subSystemPower == PowerLevel.NoPower){
                    subSystemPower = PowerLevel.MinimalPower;
                }
                return subSystemPower;
            }
        Debug.LogError($"Device not in any subsystem bounds {pos}");
        return PowerLevel.NoPower;
    }


    public void ChangePower(SubPowerSystem targetedSubSystem, PowerLevel targetPower)
    {
        if (targetPower != PowerLevel.NoPower){
            lastPowerLossTime = Time.time;
        }
        targetedSubSystem.power = targetPower;
        onPowerChanged.Invoke();
    }

    public void ChangePower(Vector3 targetedSubSystem, PowerLevel targetPowerLevel)
    {
        var subSystem = subPowerSystems.FirstOrDefault(subSystem => subSystem.InBounds(targetedSubSystem));
        if (subSystem == null){
            Debug.LogError("No subsystem found at position " + targetedSubSystem);
        }
        ChangePower(subSystem, targetPowerLevel);
    }

    public void SetGlobalMinimalPower(bool minimalPower)
    {
        GlobalMinimalPower = minimalPower;
        onPowerChanged.Invoke();
        onMinimalPowerChanged.Invoke();
    }

    void LosePower()
    {
        var targetedSubSystem = SubPowerSystems.Random();
        ChangePower(targetedSubSystem, PowerLevel.NoPower);
        lastPowerLossTime = Time.time;
    }
}