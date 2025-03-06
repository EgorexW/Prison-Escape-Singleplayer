using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu()]
public class AccessLevel : ScriptableObject
{
    [SerializeField] [OnValueChanged("SelfRecalculateAccessLevels")] List<AccessLevel> inheretedAccessLevels;

    [ReadOnly][SerializeField] List<AccessLevel> allInheretedAccessLevels = new();

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
            RecalculateAccessLevels(accessLevel);
        }
    }
    public List<AccessLevel> GetInheretedAccessLevels(){
        return inheretedAccessLevels;
    }
}