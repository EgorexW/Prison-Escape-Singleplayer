using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public sealed class Item : Loot
{
    [ReadOnly] public bool isHeld;
    [ReadOnly] public bool pickupable = true;

    public string Name;

    readonly List<IItemEffect> itemEffects = new();
    public Rigidbody Rigidbody{ get; private set; }

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        itemEffects.AddRange(GetComponents<IItemEffect>());
    }

    [Button]
    void StealNameFromGameObject()
    {
        Name = gameObject.name;
    }

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

    public override void Interact(Player player)
    {
        if (!pickupable){
            return;
        }
        player.PickupItem(this);
        base.Interact(player);
    }
}

public class Loot : MonoBehaviour, IInteractive
{
    [FoldoutGroup("Events")] public UnityEvent<Loot> onInteract;

    public float holdDuration;
    public float HoldDuration => holdDuration;

    public virtual void Interact(Player player)
    {
        onInteract.Invoke(this);
    }
}

interface IItemEffect
{
    void Use(Player player, bool alternative = false);
    void HoldUse(Player player, bool alternative = false);
    void StopUse(Player player, bool alternative = false);
}