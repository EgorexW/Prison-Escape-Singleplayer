using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu()]
public class AccessLevel : ScriptableObject
{
    [SerializeField] [OnValueChanged("SelfRecalculateAccessLevels")] List<AccessLevel> inheretedAccessLevels;

    [ReadOnly][ShowInInspector] HashSet<AccessLevel> allAccessLevelsContainingThis = new();
    [ReadOnly][ShowInInspector] HashSet<AccessLevel> allInheretedAccessLevels = new();

    public bool HasAccess(AccessLevel accessLevel){
        if (accessLevel == this){
            return true;
        }
        SelfRecalculateAccessLevels();  
        if (allInheretedAccessLevels.Contains(accessLevel)){
            return true;
        }
        return false;
    }
    void SelfRecalculateAccessLevels(){
        allInheretedAccessLevels.Clear();
        RecalculateAccessLevels(this);
    }
    void RecalculateAccessLevels(AccessLevel mainAccessLevel){
        foreach(AccessLevel accessLevel in mainAccessLevel.GetInheretedAccessLevels()){
            if (accessLevel == null){
                Debug.LogWarning("Access Level is null", this);
                continue;
            }
            if (allInheretedAccessLevels.Contains(accessLevel)){
                continue;
            }
            allInheretedAccessLevels.Add(accessLevel);
            accessLevel.allAccessLevelsContainingThis.Add(this);
            RecalculateAccessLevels(accessLevel);
        }
    }
    public List<AccessLevel> GetInheretedAccessLevels(){
        return inheretedAccessLevels;
    }
}