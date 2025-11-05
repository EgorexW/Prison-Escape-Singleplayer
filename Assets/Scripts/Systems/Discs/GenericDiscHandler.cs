using UnityEngine;

public class GenericDiscHandler : MonoBehaviour, IDiscHandler
{
    public void HandleDisc(Disc disc)
    {
        if (disc.gameTimeIncrease > 0){
            GameManager.i.gameTimeManager.ChangeTime(disc.gameTimeIncrease);
        }
        if (disc.unlockId){
            GameManager.i.facilityTriggers.UnlockTriggers(disc.unlockId);
        }
        if (disc.announcement){
            GameManager.i.facilityAnnouncements.AddAnnouncement(disc.announcement);
        }
    }

    public bool CanHandleDisc(Disc disc)
    {
        if (disc.unlockId){
            return true;
        }
        if (disc.gameTimeIncrease > 0){
            return true;
        }
        if (disc.announcement){
            return true;
        }
        return false;
    }
}