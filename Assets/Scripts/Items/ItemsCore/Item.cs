using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public sealed class Item : MonoBehaviour, IInteractive
{
    [ReadOnly] public bool isHeld;
    [ReadOnly] public bool pickupable = true;

    [SerializeField] Optional<Discovery> discoveryOnFirstPickup;
    public float holdDuration;

    readonly List<IItemEffect> itemEffects = new();
    public Rigidbody Rigidbody{ get; private set; }


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

    public float HoldDuration => holdDuration;

    public void Use(Player player, bool alternative = false)
    {
        foreach (var effect in itemEffects) effect.Use(player, alternative);
    }

    public void HoldUse(Player player, bool alternative = false)
    {
        foreach (var effect in itemEffects) effect.HoldUse(player, alternative);
    }

    public void StopUse(Player player, bool alternative = false)
    {
        foreach (var effect in itemEffects) effect.StopUse(player, alternative);
    }

    public Sprite GetPortrait()
    {
        var tex = RuntimePreviewGenerator.GenerateModelPreview(transform);
        var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height),
            new Vector2(tex.width / 2, tex.height / 2));
        return sprite;
    }
}

interface IItemEffect
{
    void Use(Player player, bool alternative = false);
    void HoldUse(Player player, bool alternative = false);
    void StopUse(Player player, bool alternative = false);
}