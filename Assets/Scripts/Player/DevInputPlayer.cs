using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevInputPlayer : MonoBehaviour
{
    [SerializeField][Required] Character character;

    void Awake(){
        InputActionMap inputActions = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        inputActions.FindAction("DevKey1").performed += UseDevKey1;
        inputActions.FindAction("DevKey2").performed += UseDevKey2;
        inputActions.FindAction("DevKey3").performed += UseDevKey3;
    }
    void UseDevKey1(InputAction.CallbackContext context){
        character.Heal(new Damage(100, 50));
    }
    void UseDevKey2(InputAction.CallbackContext context){

    }
    void UseDevKey3(InputAction.CallbackContext context){

    }
}
