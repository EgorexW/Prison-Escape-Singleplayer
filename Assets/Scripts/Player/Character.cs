using UnityEngine;
using UnityEngine.Events;

public partial class Character : MonoBehaviour, IDamagable
{
    [SerializeField] Transform aim;
    CharacterEvents characterEvents = new CharacterEvents(); 

    void Awake(){
        inventory = GetComponent<Inventory>();
        inventory.OnInventoryChange.AddListener(characterEvents.onInventoryChange.Invoke);
        characterController = GetComponent<CharacterController>();
        firstPersonController = GetComponent<IMover>();
        SetFirstPersonController();
    }
    public IStatusEffect[] GetStatusEffects(){
        return statusEffects.ToArray();
    }

    public CharacterEvents GetCharacterEvents()
    {
        return characterEvents;
    }

    public Transform GetAimTransform()
    {
        return aim;
    }
}
public class CharacterEvents{
    public readonly UnityEvent onInventoryChange = new();
    public readonly UnityEvent onHealthChange = new(); 
}