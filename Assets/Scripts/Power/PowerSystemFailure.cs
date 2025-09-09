using Sirenix.OdinInspector;
using UnityEngine;

public class PowerSystemFailure : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] MainPowerSystem mainPowerSystem;

    [SerializeField] float timeBetweenPowerLoss = 90f;

    float lastPowerLossTime;

    void Start()
    {
        lastPowerLossTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastPowerLossTime > timeBetweenPowerLoss){
            LosePower();
        }
    }

    void LosePower()
    {
        var targetedSubSystem = mainPowerSystem.SubPowerSystems.Random();
        mainPowerSystem.ChangePower(targetedSubSystem, PowerLevel.NoPower);
        lastPowerLossTime = Time.time;
    }
}