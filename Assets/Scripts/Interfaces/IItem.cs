using UnityEngine;

public interface IItem : IItemObserver{
    Sprite GetPortrait();
}

public interface IItemObserver
{
    void Use(ICharacter character, bool alternative = false);
    void HoldUse(ICharacter character, bool alternative = false);
    void StopUse(ICharacter character, bool alternative = false);
    void OnPickup(ICharacter character);
    void OnEquip(ICharacter character);
    void OnDeequip(ICharacter character);
    void OnDrop(ICharacter character, Vector3 force);
}