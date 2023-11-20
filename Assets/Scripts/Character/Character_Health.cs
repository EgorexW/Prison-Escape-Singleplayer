using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public partial class Character
{
    [ReadOnly] Health health;

    public void Damage(Damage damage)
    {   
        UpdateHealth();
        health.Damage(damage);
    }
    public void Heal(Damage damage)
    {
        UpdateHealth();
        health.Heal(damage);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    
    private void UpdateHealth()
    {
        characterEvents.onHealthChange.Invoke();
    }
    public Health GetHealth()
    {
        return health;
    }
}
