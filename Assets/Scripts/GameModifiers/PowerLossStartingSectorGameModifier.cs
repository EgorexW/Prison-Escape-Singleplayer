using UnityEngine;

public class PowerLossStartingSectorGameModifier : GameModifierSpecial
{
    PowerLevel targetPowerLevel = PowerLevel.NoPower;
    
    public override void ApplyAfterInit()
    {
        var pos = GameManager.i.Player.transform.position;
        MainPowerSystem.i.ChangePower(pos, targetPowerLevel);
    }
}
