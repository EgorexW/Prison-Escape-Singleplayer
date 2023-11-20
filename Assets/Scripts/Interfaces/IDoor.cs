using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoor : IInteractive
{
    bool CanCharacterUse(ICharacter character, bool v);
    void LockState(bool v);
    void Open();
}
