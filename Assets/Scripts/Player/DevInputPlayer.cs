using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevInputPlayer : NetworkBehaviour
{
    private ICharacter character;

    void Awake(){
        character = GetComponent<ICharacter>();
        InputActionMap inputActions = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        inputActions.FindAction("DevKey1").performed += UseDevKey1;
        inputActions.FindAction("DevKey2").performed += UseDevKey2;
        inputActions.FindAction("DevKey3").performed += UseDevKey3;
    }
    void UseDevKey1(InputAction.CallbackContext context){
        character.Damage(new Damage(10, 5));
    }
    void UseDevKey2(InputAction.CallbackContext context){

    }
    void UseDevKey3(InputAction.CallbackContext context){

    }
}
