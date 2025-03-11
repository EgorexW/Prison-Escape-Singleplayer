using UnityEngine;

public abstract class UseableItem : Item
{
    [SerializeField] float useTime;
    protected Character character;
    float startUseTime = Mathf.Infinity;

    void Update(){
        if (character == null){
            return;
        }
        if (Time.time - startUseTime < useTime){
            character.onHoldInteraction.Invoke(Time.time - startUseTime, useTime);
            return;
        }
        character.onFinishInteraction.Invoke();
        Apply();
    }

    protected abstract void Apply();

    public override void Use(Character character, bool alternative = false)
    {
        if (!alternative){
            this.character = character;
            base.Use(character);
            startUseTime = Time.time;
        } else{
            base.StopUse(character);
            character.onFinishInteraction.Invoke();
            this.character = null;
            startUseTime = Mathf.Infinity;
        }
    }
}