using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(Shooting))]
public class Weapon : Item
{
    [GetComponent][SerializeField] Shooting shooting;
    
    public override void HoldUse(Character character, bool alternative = false)
    {
        base.HoldUse(character, alternative);
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        shooting.Shoot(ray);
    }
}
