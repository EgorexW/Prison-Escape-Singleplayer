using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class MainPowerSystem : MonoBehaviour, IPowerSource
{
    [SerializeField] List<PowerLevel> startPowerDistribution;
    [SerializeField] List<SubPowerSystem> subPowerSystems;
    [SerializeField] Optional<PowerLevel> defaultPower = PowerLevel.NoPower;

    [FoldoutGroup("Events")] public UnityEvent onPowerChanged = new();

    [FoldoutGroup("Events")] public UnityEvent onMinimalPowerChanged = new();

    public static MainPowerSystem i{ get; private set; }

    public List<SubPowerSystem> SubPowerSystems => subPowerSystems.Copy();
    [ShowInInspector] [BoxGroup("Debug")] public bool GlobalMinimalPower{ get; private set; }

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
        startPowerDistribution.Shuffle();
        for (var i = 0; i < subPowerSystems.Count; i++)
            ChangePower(subPowerSystems[i], startPowerDistribution[i % startPowerDistribution.Count]);
    }

    public UnityEvent OnPowerChanged => onPowerChanged;

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
        if (!defaultPower.Enabled){
            Debug.LogWarning($"Device not in any subsystem bounds {pos}");
        }
        return defaultPower;
    }


    public void ChangePower(SubPowerSystem targetedSubSystem, PowerLevel targetPower)
    {
        targetedSubSystem.power = targetPower;
        onPowerChanged.Invoke();
    }

    public void ChangePower(Vector3 targetedPos, PowerLevel targetPowerLevel)
    {
        var subSystem = subPowerSystems.FirstOrDefault(subSystem => subSystem.InBounds(targetedPos));
        if (subSystem == null){
            Debug.LogWarning("No subsystem found at position " + targetedPos);
            return;
        }
        ChangePower(subSystem, targetPowerLevel);
    }

    public void SetGlobalMinimalPower(bool minimalPower)
    {
        GlobalMinimalPower = minimalPower;
        onPowerChanged.Invoke();
        onMinimalPowerChanged.Invoke();
    }
}