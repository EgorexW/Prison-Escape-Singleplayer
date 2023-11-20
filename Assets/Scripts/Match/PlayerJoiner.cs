using System.Collections;
using System.Collections.Generic;
using FishNet.Component.Spawning;
using FishNet.Object;
using UnityEngine;
using UnityEngine.Events;

public class PlayerJoiner : NetworkBehaviour
{
    public PlayerSpawner playerSpawner;
    public UnityEvent<ICharacter> onPlayerJoin;

    void Awake(){
        playerSpawner.OnSpawned += OnPlayerJoin;
    }
    void OnPlayerJoin(NetworkObject networkObject){
        if (!base.IsServer){
            return;
        }
        Debug.Log("Spawned Player", networkObject);
        onPlayerJoin.Invoke(networkObject.GetComponent<ICharacter>());
    }
}