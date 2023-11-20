using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKeycard{
    public bool HasAccess(AccessLevel requestedAccessLevel);
    public bool CanOpenWhenHeld();
}