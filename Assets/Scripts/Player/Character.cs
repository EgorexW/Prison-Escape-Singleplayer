using UnityEngine;

public partial class Character : MonoBehaviour, IDamagable
{
    [SerializeField] Transform aim;

    void Awake(){
        inventory = GetComponent<Inventory>();
        inventory.OnInventoryChange.AddListener(onInventoryChange.Invoke);
        characterController = GetComponent<CharacterController>();
        firstPersonController = GetComponent<IMover>();
        SetFirstPersonController();
    }
    public IStatusEffect[] GetStatusEffects(){
        return statusEffects.ToArray();
    }

    public Transform GetAimTransform()
    {
        return aim;
    }
}