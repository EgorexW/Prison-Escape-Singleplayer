using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Component.Transforming;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class RoleAssigner : MonoBehaviour
{
    public Role[] roles;
    int index = 0;

    public void AssignRoles(ICharacter[] characters){
        foreach (ICharacter character in characters)
        {
            AssignRole(character);
        }
    }

    public void AssignRole(ICharacter character)
    {
        if (index >= roles.Length)
        {
            index = 0;
        }
        Role role = roles[index];
        index += 1;
        character.SetRole(role);
        character.SetPos(role.roleSpawner.GetSpawnPoint().position);
    }
}