public class PowerGenerator : UseableItem
{
    protected override void Apply()
    {
        var mainPowerSystem = MainPowerSystem.i;
        if (mainPowerSystem.GetPower(transform.position) == PowerLevel.FullPower) return;
        mainPowerSystem.ChangePower(transform.position, PowerLevel.FullPower);
        DestroyItem();
    }
}