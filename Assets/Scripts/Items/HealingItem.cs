using UnityEngine;

public class HealingItem : Item
{
    [SerializeField] Damage heal;
    [SerializeField] Optional<HealOvertime> healOvertime;
    [SerializeField] float useTime;
    Character character;
    float startUseTime = Mathf.Infinity;

    void Update(){
        if (Time.time - startUseTime < useTime){
            return;
        }
        Heal();
    }

    protected virtual void Heal()
    {
        character.Heal(heal);
        if (healOvertime){
            character.AddStatusEffect(healOvertime.Value);
        }
        character.RemoveItem(this);
        Destroy(gameObject);
    }

    public override void Use(Character character, bool alternative = false)
    {
        if (!alternative){
            Use(character);
        } else {
            StopUse();
        }
        void Use(Character character)
        {
            this.character = character;
            base.Use(character);
            startUseTime = Time.time;
        }
        void StopUse(){
            base.StopUse(character);
            startUseTime = Mathf.Infinity;
        }
    }
}
