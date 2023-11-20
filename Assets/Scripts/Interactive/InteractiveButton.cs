using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractiveButton : MonoBehaviour, IInteractive
{
    [SerializeField] [RequireInterface(typeof(IInteractive))] GameObject interactive;
    IInteractive Interactive => (IInteractive)interactive.GetComponent<IInteractive>();

    public void Interact(ICharacter character)
    {
        Interactive.Interact(character);
    }
    public float GetHoldDuration(){
        return 0;
    }
}
