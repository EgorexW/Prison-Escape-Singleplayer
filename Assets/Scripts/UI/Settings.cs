using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class Settings : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] GameObject settingsMenu;

    void Awake()
    {
        var inputActions = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        inputActions.FindAction("Settings").performed += ToggleSettings;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}