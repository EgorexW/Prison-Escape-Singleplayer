using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour{
    [BoxGroup("References")] [Required] [SerializeField] GameObject settingsMenu;
    [BoxGroup("References")][Required][SerializeField] PlayerInput playerInput;

    void Awake(){
        HideCursor();
    }

    public void ToggleSettings(){
        if (settingsMenu.activeSelf){
            Unpause();
        }
        else{
            Pause();
        }
    }

    void ShowCursor()
    {
        playerInput.SwitchCurrentActionMap("UI");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Unpause()
    {
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
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
        AudioListener.pause = true;
        ShowCursor();
    }
}