using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class UseableItem : ItemEffect
{
    [SerializeField] float useTime;

    [FoldoutGroup("Events")] public UnityEvent onUse;

    [SerializeField] Sound soundEffect;
    
    protected Player player;
    float startUseTime = Mathf.Infinity;

    void Update()
    {
        if (player == null){
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
        player = playerTmp;
        base.Use(playerTmp);
        startUseTime = Time.time;
    }
    
    public override void StopUse(Player playerTmp, bool alternative = false)
    {
        playerTmp.onFinishInteraction.Invoke();
        player = null;
        startUseTime = Mathf.Infinity;
    }

    protected void DestroyItem()
    {
        player.RemoveItem(Item);
        Destroy(gameObject);
    }
}