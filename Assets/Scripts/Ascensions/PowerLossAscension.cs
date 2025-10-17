using System;
using UnityEngine;

public class PowerLossAscension : AscensionEffectSpecial
{
    [SerializeField] float timeBetweenPowerLoss = 90f;
    
    float lastPowerLoss;
    
    bool active;
        public override void Apply()
        {
            active = true;
        }

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
}