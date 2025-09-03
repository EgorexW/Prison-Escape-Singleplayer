using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Item : MonoBehaviour, IDamagable, IInteractive
{
    public Rigidbody Rigidbody { get; private set; }

    [ReadOnly] public bool isHeld = false;
    [ReadOnly] public bool pickupable = true;

    [SerializeField] Optional<Discovery> discoveryOnFirstPickup;
    public float holdDuration;


    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact(Player player)
    {
        if (!pickupable){
            return;
        }
        if (discoveryOnFirstPickup){
            GameDirector.i.PlayerDiscovery(discoveryOnFirstPickup);
            discoveryOnFirstPickup.Enabled = false;
        }
        player.PickupItem(this);
    }

    public virtual void Use(Player player, bool alternative = false)
    {
        // Implement item usage behavior here.
    }

    public virtual Sprite GetPortrait()
    {
        var tex = RuntimePreviewGenerator.GenerateModelPreview(transform);
        var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
        return sprite;
    }

    public virtual void HoldUse(Player player, bool alternative = false)
    {
        // Implement hold use behavior here, if necessary
    }

    public void StopUse(Player player, bool alternative = false)
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
    public float HoldDuration => holdDuration;
}