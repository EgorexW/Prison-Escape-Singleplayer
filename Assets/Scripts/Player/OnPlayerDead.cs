using UnityEngine;

public class OnPlayerDead : MonoBehaviour
{
    public void PlayerDead()
    {
        GetComponent<PlayButton>()?.Play();
    }
}