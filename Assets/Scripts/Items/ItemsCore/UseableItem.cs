using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class UseableItem : ItemEffect
{
    [SerializeField] float useTime;

    [FoldoutGroup("Events")] public UnityEvent onUse;

    [SerializeField] Sound soundEffect;

    bool charging = false;
    float startUseTime = Mathf.Infinity;

    void Update()
    {
        if (!charging){
            return;
        }
        if (Time.time - startUseTime < useTime){
            player.onHoldInteraction.Invoke(Time.time - startUseTime, useTime);
            return;
        }
        player.onFinishInteraction.Invoke();
        Apply();
        OnApply();
    }

    protected Player player => GameManager.i.Player;

    void OnApply()
    {
        onUse.Invoke();
        if (soundEffect != null){
            player.playerSoundEffects.PlaySoundEffect(soundEffect);
        }
        StopUse(player);
    }

    protected abstract void Apply();

    public override void Use(Player playerTmp, bool alternative = false)
    {
        base.Use(playerTmp);
        charging = true;
        startUseTime = Time.time;
    }

    public override void StopUse(Player playerTmp, bool alternative = false)
    {
        charging = false;
        playerTmp.onFinishInteraction.Invoke();
        startUseTime = Mathf.Infinity;
    }

    protected void DestroyItem()
    {
        player?.RemoveItem(Item);
        Destroy(gameObject);
    }
}