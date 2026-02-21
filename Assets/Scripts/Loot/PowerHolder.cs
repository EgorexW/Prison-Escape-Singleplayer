using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PowerHolder : UseableItem
{
    [SerializeField] bool charged;

    [FoldoutGroup("Events")]
    public UnityEvent<bool> onChargeChange;
    
    protected override void Apply()
    {
        var mainPowerSystem = MainPowerSystem.i;
        if (mainPowerSystem.GetPower(transform.position) == PowerLevel.FullPower){
            if (charged){
                return;
            }
            mainPowerSystem.ChangePower(transform.position, PowerLevel.NoPower);
            charged = true;
            onChargeChange.Invoke(charged);
        }
        else{
            if (!charged){
                return;
            }
            mainPowerSystem.ChangePower(transform.position, PowerLevel.FullPower);
            charged = false;
            onChargeChange.Invoke(charged);
        }
    }
    
    public bool IsCharged()
    {
        return charged;
    }
}
