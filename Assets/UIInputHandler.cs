using Nrjwolf.Tools.AttachAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class UIInputHandler : MonoBehaviour
{
    [GetComponent][SerializeField] PlayerInput playerInput;
    
    [BoxGroup("References")][Required][SerializeField] GameObject settings;

    private void OnEnable()
    {
        playerInput.onActionTriggered += HandleActionTriggered;
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= HandleActionTriggered;
    }

    private void HandleActionTriggered(InputAction.CallbackContext context)
    {
        if (!context.performed){
            return;
        }
        if (context.action.name != "Settings"){
            return;
        }
        if (settings.activeSelf){
            settings.SetActive(false);
            playerInput.SwitchCurrentActionMap("Player");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        else{
            settings.SetActive(true);
            playerInput.SwitchCurrentActionMap("UI");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.5f;
        }
    }
}
