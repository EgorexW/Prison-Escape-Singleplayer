using UnityEngine;

public class PlayerOverlayItem : ItemEffect
{
    [SerializeField] PlayerOverlay playerOverlay = PlayerOverlay.Map;
        
    public override void Use(Player player, bool alternative = false)
    {
        base.Use(player, alternative);
        player.playerOverlays.SelectOverlay(playerOverlay);
    }

    public override void StopUse(Player player, bool alternative = false)
    {
        base.StopUse(player, alternative);
        player.playerOverlays.DeselectOverlay(playerOverlay);
    }
}