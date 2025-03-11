using Sirenix.OdinInspector;
using UnityEngine;

public class Item : MonoBehaviour, IInteractive, IDamagable
{
    public Rigidbody Rigidbody { get; private set; }

    public bool isHeld = false;
    
    
    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact(Character character)
    {
        character.PickupItem(this);
    }

    public virtual void Use(Character character, bool alternative = false)
    {
        // Implement item usage behavior here.
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

    public void Damage(Damage damage)
    {
        if (isHeld){
            return;
        }
        gameObject.SetActive(false);
    }

    public Health Health => new Health(1);
}