using System;
using UnityEngine;

public class Valve : InteractButton
{
    public override float HoldDuration => 1;
    bool activated = false;
    
    void Start()
    {
        if (GameManager.i == null){
            Debug.LogWarning("No Valves Manager found in the scene.");
            Destroy(gameObject);
            return;
        }
        GameManager.i.valvesManager.Register(this);
    }

    public override void OnClick(Player player)
    {
        if (activated){
            return;
        }
        base.OnClick(player);
        GameManager.i.valvesManager.ValveActivated(this);
    }
}
