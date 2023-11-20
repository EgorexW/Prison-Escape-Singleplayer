using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using NaughtyAttributes;
using UnityEngine;

public partial class Character
{
    [ReadOnly][SyncVar(OnChange = "SyncHealth")] Health health;

    public void Damage(Damage damage)
    {
        OwnerDamage(damage);
    }
    public void Heal(Damage damage){
        OwnerHeal(damage);
    }
    [ServerRpc(RequireOwnership = false)]
    private void OwnerDamage(Damage damage)
    {
        Debug.Log("Damage " + damage);
        health.Damage(damage);
    }
    [ServerRpc(RequireOwnership = false)]
    private void OwnerHeal(Damage damage)
    {
        health.Heal(damage);
    }
    [ServerRpc(RequireOwnership = false)]
    private void Die()
    {
        Despawn(gameObject);
    }
    void SyncHealth(Health prev, Health next, bool asServer)
    {
        if (asServer){
            return;
        }
        UpdateHealth();
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
