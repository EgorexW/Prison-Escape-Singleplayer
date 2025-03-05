using Sirenix.OdinInspector;
using UnityEngine;

public class Item : MonoBehaviour, IInteractive
{
    [SerializeField] Vector3 equipPos;
    [SerializeField] Vector3 equipRotation;
    [SerializeField] float equipScaleChange = 1;
    [SerializeField] float equipTime = 0.5f;
    new Rigidbody rigidbody;
    const float ContinuousCollisionDetectionTimeOnThrow = 2f;

#if UNITY_EDITOR
    [Button]
    void CopyFromTransform(){
        equipPos = transform.localPosition;
        equipRotation = transform.localRotation.eulerAngles;
    }
#endif
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void Interact(Character character)
    {
        character.PickupItem(this);
    }
    public virtual void Use(Character character, bool alternative = false)
    {

    }

    public void OnPickup(Character character)
    {
        gameObject.SetActive(false);
        transform.SetParent(character.GetAimTransform());
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;     
        rigidbody.isKinematic = true; 
    }

    public void OnEquip(Character character)
    {
        gameObject.SetActive(true);
        transform.LeanMoveLocal(equipPos, equipTime);
        transform.localRotation = Quaternion.Euler(equipRotation);
        transform.localScale *= equipScaleChange;
    }

    public void OnDeequip(Character character)
    {
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity; 
        transform.localScale *= 1 / equipScaleChange; 
    }

    public void OnDrop(Character character, Vector3 force)
    {
        gameObject.SetActive(true);   
        transform.SetParent(null);
        rigidbody.isKinematic = false;
        rigidbody.AddForce(force, ForceMode.Impulse); 
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        General.CallAfterSeconds(() => rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete, ContinuousCollisionDetectionTimeOnThrow);

    }

    public virtual Sprite GetPortrait()
    {
        Texture2D tex = RuntimePreviewGenerator.GenerateModelPreview(transform);
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
        return sprite;
    }

    public virtual void HoldUse(Character character, bool alternative = false)
    {

    }

    public void StopUse(Character character, bool alternative = false)
    {

    }
}
