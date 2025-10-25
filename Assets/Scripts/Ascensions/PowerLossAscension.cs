using UnityEngine;

public class PowerLossAscension : AscensionEffectSpecial
{
    [SerializeField] float timeBetweenPowerLoss = 90f;

    bool active;

    float lastPowerLoss;

    void Update()
    {
        if (!active){
            return;
        }
        if (GameManager.i.gameTimeManager.GameTime < lastPowerLoss + timeBetweenPowerLoss){
            return;
        }

        lastPowerLoss = GameManager.i.gameTimeManager.GameTime;
        MainPowerSystem.i.ChangePower(MainPowerSystem.i.SubPowerSystems.Random(), PowerLevel.NoPower);
    }

    public override void Apply()
    {
        active = true;
    }
}