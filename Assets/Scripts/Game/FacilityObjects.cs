using Sirenix.OdinInspector;
using UnityEngine;

public class FacilityObjects : MonoBehaviour
{
    [Button]
    [HideInEditorMode]
    public void UnlockSwitch(string switchId)
    {
        var switches = GetSwitches();
        foreach (var facilitySwitch in switches)
            if (facilitySwitch.id == switchId){
                facilitySwitch.Activate();
            }
    }
    
    public static FacilityObject GetSwitch(string switchId)
    {
        var switches = GetSwitches();
        foreach (var facilitySwitch in switches)
            if (facilitySwitch.id == switchId){
                return facilitySwitch;
            }
        return null;
    }
    static FacilityObject[] GetSwitches()
    {
        return FindObjectsByType<FacilityObject>(FindObjectsSortMode.None);
    }
}