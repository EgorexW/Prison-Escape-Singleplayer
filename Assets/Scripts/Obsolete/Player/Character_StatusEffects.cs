using System.Collections.Generic;

public partial class Player
{
    readonly List<IStatusEffect> statusEffects = new();

    void Update()
    {
        foreach (var statusEffect in statusEffects.ToArray()) statusEffect.OnUpdate(this);
    }

    public void AddStatusEffect(IStatusEffect statusEffect)
    {
        var canAdd = true;
        foreach (var existingStatusEffect in
                 statusEffects.FindAll(status => status.GetType() == statusEffect.GetType()))
            if (!existingStatusEffect.CanAddCopy(this, statusEffect)){
                canAdd = false;
            }
        if (!canAdd){
            return;
        }
        statusEffect.OnApply(this);
        statusEffects.Add(statusEffect);
    }

    public void RemoveStatusEffect(IStatusEffect statusEffect)
    {
        statusEffect.OnRemove(this);
        statusEffects.Remove(statusEffect);
    }
}