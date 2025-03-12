using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu()]
public class AccessLevel : ScriptableObject
{
    [SerializeField] List<AccessLevel> inheretedAccessLevels;

    [ShowInInspector] HashSet<AccessLevel> AllAccessLevels => GetAllAccessLevels(this);

    public bool HasAccess(AccessLevel accessLevel){
        if (accessLevel == this){
            return true;
        }
        return GetAllAccessLevels(this).Contains(accessLevel);
    }
    HashSet<AccessLevel> GetAllAccessLevels(AccessLevel mainAccessLevel)
    {
        HashSet<AccessLevel> allInheretedAccessLevels = new();
        foreach(AccessLevel accessLevel in mainAccessLevel.GetInheretedAccessLevels()){
            if (accessLevel == null){
                Debug.LogWarning("Access Level is null", this);
                continue;
            }
            if (allInheretedAccessLevels.Contains(accessLevel)){
                continue;
            }
            allInheretedAccessLevels.Add(accessLevel);
            GetAllAccessLevels(accessLevel);
        }
        return allInheretedAccessLevels;
    }
    public List<AccessLevel> GetInheretedAccessLevels(){
        return inheretedAccessLevels;
    }
}