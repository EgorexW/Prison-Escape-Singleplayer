using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Tutorials : MonoBehaviour
{
    [FormerlySerializedAs("tutorialUI")] [BoxGroup("References")][Required][SerializeField] TutorialsUI tutorialsUI;
    
    [BoxGroup("Config")][SerializeField] float movementTutorialDelay = 5f;
    [BoxGroup("Config")][SerializeField] float interactTutorialDelay = 10f;
    [BoxGroup("Config")][SerializeField] float sprintTutorialDelay = 60f;
    [BoxGroup("Config")][SerializeField] float swapItemTutorialDelay = 45f;
    [BoxGroup("Config")][SerializeField] float throwTutorialDelay = 90f;
    [BoxGroup("Config")][SerializeField] float useTutorialDelay = 30f;
    [BoxGroup("Config")][SerializeField] float reminderInteractCooldown = 30f;
    [BoxGroup("Config")][SerializeField] float reminderInteractPlayerDelay = 3f;
        
    Vector3 playerStartPos;
    Player player;

    float playerInteractAimStartTime;
    float lastInteractTime;
    IInteractive lastInteractive;

    public void Activate()
    {
        player = GameManager.i.Player;
        playerStartPos = player.transform.position; 
        player.onFinishInteraction.AddListener(OnInteractionFinished);
        player.onSwapItem.AddListener(OnSwapItem);
        player.onThrowItem.AddListener(OnThrowItem);
        player.onUseItem.AddListener(OnUseItem);
    }
    void OnUseItem()
    {
        if (!tutorialsUI.UseTutorial){
            return;
        }
        PlayerPrefs.SetInt("Tutorial/Use", 1);
        tutorialsUI.UseTutorial = false;
    }

    void OnThrowItem()
    {
        if (!tutorialsUI.ThrowTutorial){
            return;
        }
        PlayerPrefs.SetInt("Tutorial/Throw", 1);
        tutorialsUI.ThrowTutorial = false;
    }

    void OnSwapItem()
    {
        if (!tutorialsUI.SwapItemTutorial){
            return;
        }
        PlayerPrefs.SetInt("Tutorial/Swap", 1);
        tutorialsUI.SwapItemTutorial = false;
    }

    void OnInteractionFinished()
    {
        lastInteractTime = Time.time;
        if (!tutorialsUI.InteractTutorial){
            return;
        }
        PlayerPrefs.SetInt("Tutorial/Interact", 1);
        tutorialsUI.InteractTutorial = false;
    }

    void Update()
    {
        if (player == null){
            return;
        }
        ResolveMovementTutorial();
        ResolveInteractTutorial();
        ResolveSprintTutorial();
        ResolveSwapItemTutorial();
        ResolveThrowTutorial();
        ResolveUseTutorial();
    }

    void ResolveUseTutorial()
    {
        if (GameManager.i.gameTimeManager.GameTime <= useTutorialDelay){
            return;
        }
        if (PlayerPrefs.GetInt("Tutorial/Use", 0) == 1){
            return;
        }
        var heldItem = player.GetHeldItem();
        if (heldItem == null || heldItem.Name == ""){
            tutorialsUI.UseTutorial = false;
            return;
        }
        tutorialsUI.UseTutorial = true;
    }

    void ResolveThrowTutorial()
    {
        if (GameManager.i.gameTimeManager.GameTime <= throwTutorialDelay){
            return;
        }
        if (PlayerPrefs.GetInt("Tutorial/Throw", 0) == 1){
            return;
        }
        tutorialsUI.ThrowTutorial = player.GetHeldItem() != null;
    }

    void ResolveSwapItemTutorial()
    {
        if (GameManager.i.gameTimeManager.GameTime <= swapItemTutorialDelay){
            return;
        }
        if (PlayerPrefs.GetInt("Tutorial/Swap", 0) == 1){
            return;
        }
        tutorialsUI.SwapItemTutorial = player.GetInventory().GetItems().Count > 1;
    }

    void ResolveSprintTutorial()
    {
        if (GameManager.i.gameTimeManager.GameTime <= sprintTutorialDelay){
            return;
        }
        if (PlayerPrefs.GetInt("Tutorial/Sprint", 0) == 1){
            return;
        }
        if (player.Stamina >= 0.95){
            tutorialsUI.SprintTutorial = true;
        }
        else{
            if (tutorialsUI.SprintTutorial){
                PlayerPrefs.SetInt("Tutorial/Sprint", 1);
            }
            tutorialsUI.SprintTutorial = false;
        }
    }

    void ResolveInteractTutorial()
    {
        ResolveReminderInteractTutorial();
        if (GameManager.i.gameTimeManager.GameTime <= interactTutorialDelay){
            return;
        }
        if (PlayerPrefs.GetInt("Tutorial/Interact", 0) == 1){
            return;
        }
        tutorialsUI.InteractTutorial = !player.GetInteractive().IsDummy;
    }

    void ResolveReminderInteractTutorial()
    {
        if (PlayerPrefs.GetInt("Tutorial/Interact", 0) == 0){
            return;
        }
        tutorialsUI.InteractTutorial = false;
        if (Time.time - lastInteractTime < reminderInteractCooldown && !tutorialsUI.InteractTutorial){
            return;
        }
        var currentInteractive = player.GetInteractive();
        if (currentInteractive.IsDummy){
            lastInteractive = null;
            return;
        }
        if (lastInteractive != currentInteractive){
            lastInteractive = currentInteractive;
            playerInteractAimStartTime = Time.time;
            return;
        }
        if (Time.time - playerInteractAimStartTime < reminderInteractPlayerDelay){
            return;
        }
        tutorialsUI.InteractTutorial = true;
    }

    void ResolveMovementTutorial()
    {
        if (GameManager.i.gameTimeManager.GameTime <= movementTutorialDelay){
            return;
        }
        var playerPos = player.transform.position;
        if (Vector3.Distance(playerStartPos, playerPos) > 0.1f){
            if (tutorialsUI.MovementTutorial){
                PlayerPrefs.SetInt("Tutorial/Movement", 1);
                tutorialsUI.MovementTutorial = false;
            }
            return;
        }
        if (PlayerPrefs.GetInt("Tutorial/Movement", 0) == 1){
            return;
        }
        tutorialsUI.MovementTutorial = true;
    }
}