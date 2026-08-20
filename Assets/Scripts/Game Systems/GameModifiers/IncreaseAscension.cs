using UnityEngine;

public class IncreaseAscension : MonoBehaviour
{
    public void Increase()
    {
        Ascensions.SetAscensionLevel(Ascensions.AscensionLevel + 1);
    }
}