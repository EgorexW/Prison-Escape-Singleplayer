using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(Shooting))]
public class Weapon : ItemEffect
{
    [GetComponent] [SerializeField] Shooting shooting;

    public override void HoldUse(Player player, bool alternative = false)
    {
        base.HoldUse(player, alternative);
        var screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        var ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        shooting.Shoot(ray);
    }
}