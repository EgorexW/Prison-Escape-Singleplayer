using System;
using Sirenix.OdinInspector;
using UnityEngine;

class TutorialsUI : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] GameObject movementTutorialUI;
    [BoxGroup("References")][Required][SerializeField] GameObject interactTutorialUI;
    [BoxGroup("References")][Required][SerializeField] GameObject sprintTutorialUI;
    [BoxGroup("References")][Required][SerializeField] GameObject useTutorialUI;
    [BoxGroup("References")][Required][SerializeField] GameObject swapItemTutorialUI;
    [BoxGroup("References")][Required][SerializeField] GameObject throwTutorialUI;
    [BoxGroup("References")][Required][SerializeField] GameObject alternateUseTutorialUI;

    void Awake()
    {
        MovementTutorial = false;
        InteractTutorial = false;
        SprintTutorial = false;
        UseTutorial = false;
        SwapItemTutorial = false;
        ThrowTutorial = false;
        AlternateUseTutorial = false;
    }

    public bool MovementTutorial{
        get => movementTutorialUI.activeSelf;
        set{
            movementTutorialUI.SetActive(value);
        }
    }

    public bool InteractTutorial{
        get => interactTutorialUI.activeSelf;
        set{
            interactTutorialUI.SetActive(value);
        }
    }

    public bool SprintTutorial{
        get => sprintTutorialUI.activeSelf;
        set{
            sprintTutorialUI.SetActive(value);
        }
    }

    public bool UseTutorial{
        get => useTutorialUI.activeSelf;
        set{
            useTutorialUI.SetActive(value);
        }
    }

    public bool SwapItemTutorial{
        get => swapItemTutorialUI.activeSelf;
        set{
            swapItemTutorialUI.SetActive(value);
        }
    }

    public bool ThrowTutorial{
        get => throwTutorialUI.activeSelf;
        set{
            throwTutorialUI.SetActive(value);
        }
    }

    public bool AlternateUseTutorial{
        get => alternateUseTutorialUI.activeSelf;
        set{
            alternateUseTutorialUI.SetActive(value);
        }
    }
}