using Sirenix.OdinInspector;
using UnityEngine;

public class PowerEffects : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] MainPowerSystem mainPowerSystem;

    [SerializeField] FacilityAnnouncement minimalPowerOff;
    [SerializeField] FacilityAnnouncement minimalPowerOn;

    void Awake()
    {
        mainPowerSystem.OnMinimalPowerChanged.AddListener(OnMinimalPowerChanged);
    }

    void OnMinimalPowerChanged()
    {
        if (MainPowerSystem.i.GlobalMinimalPower){
            GameDirector.i.facilityAnnouncements.AddAnnouncement(minimalPowerOn);
        }
        else{
            GameDirector.i.facilityAnnouncements.AddAnnouncement(minimalPowerOff);
        }
    }
}