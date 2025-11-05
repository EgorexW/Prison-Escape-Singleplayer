using Sirenix.OdinInspector;
using UnityEngine;

public class PowerEffects : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] MainPowerSystem mainPowerSystem;

    [SerializeField] FacilityAnnouncement minimalPowerOff;
    [SerializeField] FacilityAnnouncement minimalPowerOn;

    void Awake()
    {
        mainPowerSystem.onMinimalPowerChanged.AddListener(OnMinimalPowerChanged);
    }

    void OnMinimalPowerChanged()
    {
        if (MainPowerSystem.i.GlobalMinimalPower){
            GameManager.i.facilityAnnouncements.AddAnnouncement(minimalPowerOn);
        }
        else{
            GameManager.i.facilityAnnouncements.AddAnnouncement(minimalPowerOff);
        }
    }
}