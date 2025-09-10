using UnityEngine;

public class PowerGenerator : UseableItem
{
    protected override void Apply()
    {
        var mainPowerSystem = MainPowerSystem.i;
        mainPowerSystem.ChangePower(transform.position, PowerLevel.FullPower);
        DestroyItem();
    }
}
