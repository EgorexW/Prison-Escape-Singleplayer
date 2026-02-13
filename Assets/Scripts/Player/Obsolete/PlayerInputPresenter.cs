using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
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
        var inputActions = playerInput.actions.FindActionMap("Player");
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
        inputActions.FindAction("Settings").performed += ToggleSettings;
        playerInput.actions.FindActionMap("UI").FindAction("Cancel").performed += ToggleSettings;
        HideCursor();
    }

    void Update()
    {
        HoldUse(useAction);
        AlternativeHoldUse(alternativeUseAction);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (Time.timeScale <= 0){
            return;
        }
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
        if (Time.timeScale <= 0){
            return;
        }
        player.UseHeldItem();
    }

    void HoldUse(InputAction action)
    {
        if (Time.timeScale <= 0){
            return;
        }
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
        if (Time.timeScale <= 0){
            return;
        }
        player.UseHeldItem(true);
    }

    void AlternativeHoldUse(InputAction action)
    {
        if (Time.timeScale <= 0){
            return;
        }
        if (!action.IsPressed()){
            return;
        }
        player.HoldUseHeldItem(true);
    }

    void DropEquipedItem(InputAction.CallbackContext context)
    {
        if (Time.timeScale <= 0){
            return;
        }
        if (!context.performed){
            return;
        }
        player.ThrowItem();
    }

    void ChangeItem(InputAction.CallbackContext context)
    {
        if (Time.timeScale <= 0){
            return;
        }
        if (!context.performed){
            return;
        }
        player.SwapItem();
    }
    
    [BoxGroup("References")] [Required] [SerializeField] GameObject settingsMenu;
    [BoxGroup("References")][Required][SerializeField] PlayerInput playerInput;

    void OnDisable()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ShowCursor()
    {
        playerInput.SwitchCurrentActionMap("UI");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ToggleSettings(InputAction.CallbackContext obj)
    {
        if (!obj.performed){
            return;
        }
        if (settingsMenu.activeSelf){
            Unpause();
        }
        else{
            Pause();
        }
    }

    void Unpause()
    {
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        HideCursor();
    }

    void HideCursor()
    {
        playerInput.SwitchCurrentActionMap("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
        ShowCursor();
    }
}