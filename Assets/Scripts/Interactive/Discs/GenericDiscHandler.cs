using UnityEngine;

public class GenericDiscHandler : MonoBehaviour, IDiscHandler
{
    public void HandleDisc(Disc disc)
    {
        if (disc.gameTimeIncrease > 0)
        {
            GameDirector.i.gameTime.IncreaseTime(disc.gameTimeIncrease);
        }
    }

    public bool CanHandleDisc(Disc disc)
    {
        return disc.gameTimeIncrease > 0;
    }
}