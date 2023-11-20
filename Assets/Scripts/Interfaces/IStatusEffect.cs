using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    void OnApply(ICharacter character);
    void OnUpdate(ICharacter character);
    void OnRemove(ICharacter character);
    bool CanAddCopy(ICharacter character, IStatusEffect copy);
}
