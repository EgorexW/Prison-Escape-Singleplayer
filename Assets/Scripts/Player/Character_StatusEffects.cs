using System.Collections.Generic;

public partial class Character
{
    List<IStatusEffect> statusEffects = new();
    void Update(){
        foreach (IStatusEffect statusEffect in statusEffects.ToArray())
        {
            statusEffect.OnUpdate(this);
        }
    }

    public void AddStatusEffect(IStatusEffect statusEffect){
        bool canAdd = true;
        foreach (IStatusEffect existingStatusEffect in statusEffects.FindAll(status => status.GetType() == statusEffect.GetType()))
        {
            if (!existingStatusEffect.CanAddCopy(this, statusEffect)){
                canAdd = false;
            }
        }
        if (!canAdd){
            return;
        }
        statusEffect.OnApply(this);
        statusEffects.Add(statusEffect);
    }
    public void RemoveStatusEffect(IStatusEffect statusEffect){
        statusEffect.OnRemove(this);
        statusEffects.Remove(statusEffect);
    }
}
