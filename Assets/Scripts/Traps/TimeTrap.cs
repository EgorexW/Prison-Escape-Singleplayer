using Sirenix.OdinInspector;
using UnityEngine;

public class TimeTrap : PoweredDevice, ITrap
{
    [BoxGroup("References")] [Required] [SerializeField] MotionSensor motionSensor;

    [SerializeField] float timePenalty = 60f;
    [SerializeField] Optional<FacilityAnnouncement> announcement;

    [SerializeField] bool isActive;

    void Awake()
    {
        motionSensor.onActivation.AddListener(OnTriggered);
    }

    public void Activate()
    {
        isActive = true;
        OnPowerChanged();
    }

    public void OnTriggered()
    {
        GameManager.i.gameTime.ChangeTime(-timePenalty);
        if (announcement){
            GameManager.i.facilityAnnouncements.AddAnnouncement(announcement);
        }
        isActive = false;
        OnPowerChanged();
    }

    protected override void OnPowerChanged()
    {
        base.OnPowerChanged();
        motionSensor.SetActive(IsPowered() && isActive);
    }
}