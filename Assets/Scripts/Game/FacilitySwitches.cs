using Sirenix.OdinInspector;
using UnityEngine;

public class FacilitySwitches : MonoBehaviour
{
    [Button][HideInEditorMode]
    public void UnlockSwitch(string switchId)
    {
        FacilitySwitch[] switches = FindObjectsByType<FacilitySwitch>(FindObjectsSortMode.None);
        foreach (var facilitySwitch in switches)
        {
            if (facilitySwitch.id == switchId)
            {
                facilitySwitch.Activate();
            }
        }
    }
}
