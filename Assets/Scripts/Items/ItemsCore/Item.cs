using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public sealed class Item : MonoBehaviour, IInteractive
{
    public Rigidbody Rigidbody { get; private set; }
    
    List<IItemEffect> itemEffects = new List<IItemEffect>();

    [ReadOnly] public bool isHeld = false;
    [ReadOnly] public bool pickupable = true;

    [SerializeField] Optional<Discovery> discoveryOnFirstPickup;
    public float holdDuration;


    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        itemEffects.AddRange(GetComponents<IItemEffect>());
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

    public void Use(Player player, bool alternative = false)
    {
        foreach (var effect in itemEffects){
            effect.Use(player, alternative);
        }
    }

    public void HoldUse(Player player, bool alternative = false)
    {
        foreach (var effect in itemEffects){
            effect.HoldUse(player, alternative);
        }
    }
    public void StopUse(Player player, bool alternative = false)
    {
        foreach (var effect in itemEffects){
            effect.StopUse(player, alternative);
        }
    }

    public float HoldDuration => holdDuration;
    public Sprite GetPortrait()
    {
        var tex = RuntimePreviewGenerator.GenerateModelPreview(transform);
        var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
        return sprite;
    }
}

interface IItemEffect
{
    void Use(Player player, bool alternative = false);
    void HoldUse(Player player, bool alternative = false);
    void StopUse(Player player, bool alternative = false);
}