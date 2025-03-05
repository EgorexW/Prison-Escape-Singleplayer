using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputPresenter : MonoBehaviour
{
    [SerializeField] [GetComponent] Character character;
    InputAction useAction;
    private InputAction alternativeUseAction;

    void Awake(){
        character = GetComponent<Character>();
        InputActionMap inputActions = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        inputActions.FindAction("DropItem").performed += DropEquipedItem;
        inputActions.FindAction("ChangeItem").performed += ChangeItem;
        useAction = inputActions.FindAction("Use");
        useAction.performed += Use;
        useAction.canceled += StopUse;
        alternativeUseAction = inputActions.FindAction("AlternativeUse");
        alternativeUseAction.performed += AlternativeUse;
        alternativeUseAction.canceled += AlternativeStopUse;
        inputActions.FindAction("Interact").performed += OnInteract;
		inputActions.FindAction("Interact").canceled += OnInteract;
    }

    public void OnInteract(InputAction.CallbackContext context){
        if (context.performed){
            character.Interact();
        }
        if (context.canceled){
            character.CancelInteract();
        }
    }
    private void StopUse(InputAction.CallbackContext context)
    {
        character.StopUseHeldItem();
    }

    private void Use(InputAction.CallbackContext context)
    {
        character.UseHeldItem();
    }

    void HoldUse(InputAction action){
        if (!action.IsPressed()){
            return;
        }
        character.HoldUseHeldItem();
    }
    private void AlternativeStopUse(InputAction.CallbackContext context)
    {
        character.StopUseHeldItem(true);
    }

    private void AlternativeUse(InputAction.CallbackContext context)
    {
        character.UseHeldItem(true);
    }

    void AlternativeHoldUse(InputAction action){
        if (!action.IsPressed()){
            return;
        }
        character.HoldUseHeldItem(true);
    }
    void Update(){
        HoldUse(useAction);
        AlternativeHoldUse(alternativeUseAction);
    }
    void DropEquipedItem(InputAction.CallbackContext context){
        if (!context.performed){
            return;
        }
        character.DropItem();
    }
    void ChangeItem(InputAction.CallbackContext context){
        if (!context.performed){
            return;
        }
        var items = character.GetInventory().GetItems();
        if (items.Count == 0){
            return;
        }
        int index = items.IndexOf(character.GetHeldItem());
        index += 1;
        while(index < 0){
            index += items.Count;
        }
        while (index >= items.Count){
            index -= items.Count;
        }
        character.EquipItem(items[index]);
    }
}
