using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu()]
public class AccessLevel : ScriptableObject
{
    [SerializeField][OnValueChanged("RecalculateAccessLevels")] AccessLevel[] inheretedAccessLevels = new AccessLevel[0];

    [ReadOnly][SerializeField] List<AccessLevel> allInheretedAccessLevels = new();

    public bool HasAccess(AccessLevel accessLevel){
        if (accessLevel == this){
            return true;
        }
        RecalculateAccessLevels();  
        if (allInheretedAccessLevels.Contains(accessLevel)){
            return true;
        }
        return false;
    }
    void RecalculateAccessLevels(){
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
    public AccessLevel[] GetInheretedAccessLevels(){
        return inheretedAccessLevels;
    }
}