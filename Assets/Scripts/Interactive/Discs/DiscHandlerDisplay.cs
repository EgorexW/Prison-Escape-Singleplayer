using UnityEngine;

public class DiscHandlerDisplay : MonoBehaviour
{
    public void OnPowerChanged(PowerLevel powerLevel)
    {
        gameObject.SetActive(powerLevel == PowerLevel.FullPower);
    }
}
