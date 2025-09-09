using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInputPresenter : MonoBehaviour
{
    [FormerlySerializedAs("character")] [SerializeField] [GetComponent] Player player;
    InputAction alternativeUseAction;
    InputAction useAction;

    void Awake()
    {
        player = GetComponent<Player>();
        var inputActions = GetComponent<PlayerInput>().actions.FindActionMap("Player");
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

    void Update()
    {
        HoldUse(useAction);
        AlternativeHoldUse(alternativeUseAction);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed){
            player.Interact();
        }
        if (context.canceled){
            player.CancelInteract();
        }
    }

    void StopUse(InputAction.CallbackContext context)
    {
        player.StopUseHeldItem();
    }

    void Use(InputAction.CallbackContext context)
    {
        player.UseHeldItem();
    }

    void HoldUse(InputAction action)
    {
        if (!action.IsPressed()){
            return;
        }
        player.HoldUseHeldItem();
    }

    void AlternativeStopUse(InputAction.CallbackContext context)
    {
        player.StopUseHeldItem(true);
    }

    void AlternativeUse(InputAction.CallbackContext context)
    {
        player.UseHeldItem(true);
    }

    void AlternativeHoldUse(InputAction action)
    {
        if (!action.IsPressed()){
            return;
        }
        player.HoldUseHeldItem(true);
    }

    void DropEquipedItem(InputAction.CallbackContext context)
    {
        if (!context.performed){
            return;
        }
        player.ThrowItem();
    }

    void ChangeItem(InputAction.CallbackContext context)
    {
        if (!context.performed){
            return;
        }
        var items = player.GetInventory().GetItems();
        if (items.Count == 0){
            return;
        }
        var index = items.IndexOf(player.GetHeldItem());
        index += 1;
        while (index < 0) index += items.Count;
        while (index >= items.Count) index -= items.Count;
        player.EquipItem(items[index]);
    }
}