using Sirenix.OdinInspector;
using StarterAssets;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [BoxGroup("References")] [Required] public PlayerEffects playerEffects;
    [BoxGroup("References")] [Required] public PlayerHealth playerHealth;
    [BoxGroup("References")] [Required] public PlayerSoundEffects playerSoundEffects;
    [BoxGroup("References")][Required] public PlayerOverlays playerOverlays;
    [BoxGroup("References")] [Required] public Headlight headlight;
    
    [SerializeField] Transform aim;
    

    void Awake()
    {
        inventory = GetComponent<Inventory>();
        inventory.OnInventoryChange.AddListener(onInventoryChange.Invoke);
        characterController = GetComponent<CharacterController>();
        firstPersonController = GetComponent<FirstPersonController>();
        SetFirstPersonController();
        AudioListener.pause = true;
    }

    public Transform GetAimTransform()
    {
        return aim;
    }
}