using UnityEngine;

public abstract class UseableItem : ItemEffect
{
    [SerializeField] float useTime;
    protected Player player;
    float startUseTime = Mathf.Infinity;

    void Update(){
        if (player == null){
            return;
        }
        if (Time.time - startUseTime < useTime){
            player.onHoldInteraction.Invoke(Time.time - startUseTime, useTime);
            return;
        }
        player.onFinishInteraction.Invoke();
        Apply();
    }

    protected abstract void Apply();

    public override void Use(Player playerTmp, bool alternative = false)
    {
        if (!alternative){
            this.player = playerTmp;
            base.Use(playerTmp);
            startUseTime = Time.time;
        } else{
            base.StopUse(playerTmp);
            playerTmp.onFinishInteraction.Invoke();
            this.player = null;
            startUseTime = Mathf.Infinity;
        }
    }
}