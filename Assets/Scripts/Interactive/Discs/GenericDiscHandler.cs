using UnityEngine;

public class GenericDiscHandler : MonoBehaviour, IDiscHandler
{
    public void HandleDisc(Disc disc)
    {
        if (disc.gameTimeIncrease > 0)
        {
            GameDirector.i.gameTime.ChangeTime(disc.gameTimeIncrease);
        }
        if (disc.unlockId)
        {
            GameDirector.i.facilitySwitches.UnlockSwitch(disc.unlockId);
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
        return false;
    }
}