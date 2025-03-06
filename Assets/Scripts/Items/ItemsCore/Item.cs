using Sirenix.OdinInspector;
using UnityEngine;

public class Item : MonoBehaviour, IInteractive
{
    [SerializeField] Vector3 equipPos;
    [SerializeField] Vector3 equipRotation;
    [SerializeField] float equipScaleChange = 1f;
    [SerializeField] float equipTime = 0.5f;

    new Rigidbody rigidbody;
    const float CONTINUOUS_COLLISION_DETECTION_TIME_ON_THROW = 2f;

#if UNITY_EDITOR
    [Button]
    void CopyFromTransform()
    {
        equipPos = transform.localPosition;
        equipRotation = transform.localRotation.eulerAngles;
    }
#endif

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact(Character character)
    {
        character.PickupItem(this);
    }

    public virtual void Use(Character character, bool alternative = false)
    {
        // Implement item usage behavior here.
    }

    public void OnPickup(Character character)
    {
        // Disable physics and attach to character
        gameObject.SetActive(false); // Ensure item is active
        transform.SetParent(character.GetAimTransform());
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rigidbody.isKinematic = true; // Disable physics while holding
    }

    public void OnEquip(Character character)
    {
        // Ensure item is visible and move it into the equipped position
        gameObject.SetActive(true);
        transform.SetParent(character.GetAimTransform()); // Ensure it's parented to the character's aim position
        transform.LeanMoveLocal(equipPos, equipTime); // Smooth movement to equip position
        transform.localRotation = Quaternion.Euler(equipRotation);
        transform.localScale = Vector3.one * equipScaleChange; // Scale change
    }

    public void OnDeequip(Character character)
    {
        // Hide item and reset transform when unequipped
        gameObject.SetActive(false); // Disable item visibility
        transform.SetParent(null); // Remove parent
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one; // Reset scale
    }

    public void OnDrop(Character character, Vector3 force)
    {
        // Activate item, reset position, and add force
        gameObject.SetActive(true);
        transform.SetParent(null); // Detach from character
        rigidbody.isKinematic = false; // Enable physics
        rigidbody.AddForce(force, ForceMode.Impulse); // Add force for throwing
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous; // Enable continuous collision detection

        // Reset collision detection mode after a delay
        General.CallAfterSeconds(() =>
        {
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }, CONTINUOUS_COLLISION_DETECTION_TIME_ON_THROW);
    }

    public virtual Sprite GetPortrait()
    {
        Texture2D tex = RuntimePreviewGenerator.GenerateModelPreview(transform);
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
        return sprite;
    }

    public virtual void HoldUse(Character character, bool alternative = false)
    {
        // Implement hold use behavior here, if necessary
    }

    public void StopUse(Character character, bool alternative = false)
    {
        // Implement stop use behavior here, if necessary
    }
}