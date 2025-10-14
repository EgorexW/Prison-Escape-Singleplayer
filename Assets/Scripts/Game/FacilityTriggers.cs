using Sirenix.OdinInspector;
using UnityEngine;

public class FacilityTriggers : MonoBehaviour
{
    [Button]
    [HideInEditorMode]
    public void UnlockTriggers(string switchId)
    {
        var switches = GetSwitches();
        foreach (var facilitySwitch in switches)
            if (facilitySwitch.id == switchId){
                facilitySwitch.Activate();
            }
    }

    public static FacilityTrigger GetSwitch(string switchId)
    {
        var switches = GetSwitches();
        foreach (var facilitySwitch in switches)
            if (facilitySwitch.id == switchId){
                return facilitySwitch;
            }
        Debug.LogWarning("No switch found with id " + switchId);
        return null;
    }

    static FacilityTrigger[] GetSwitches()
    {
        return FindObjectsByType<FacilityTrigger>(FindObjectsSortMode.None);
    }
}