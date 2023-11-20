using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSpawnPoint : MonoBehaviour
{
    public Role role;
    void OnEnable(){
        role.roleSpawner.AddSpawnPoint(transform);
    }
    void OnDisable(){
        if (role.roleSpawner == null){
            return;
        }
        role.roleSpawner.RemoveSpawnPoint(transform);
    }
}
