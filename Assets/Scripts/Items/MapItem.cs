using UnityEngine;

public class MapItem : ItemEffect
{
    public override void Use(Player player, bool alternative = false)
    {
        base.Use(player, alternative);
        player.MapSetActive(true);
    }

    public override void StopUse(Player player, bool alternative = false)
    {
        base.StopUse(player, alternative);
        player.MapSetActive(false);
    }
}
