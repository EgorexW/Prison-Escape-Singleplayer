using UnityEngine;
using UnityEngine.Events;

public class InteractButton : MonoBehaviour, IInteractive
{
    public UnityEvent<Player> onClick;

    public void Interact(Player player)
    {
        OnClick(player);
    }

    public float HoldDuration => 3;

    public virtual void OnClick(Player player)
    {
        onClick.Invoke(player);
    }
}