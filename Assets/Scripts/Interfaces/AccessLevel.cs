using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu]
public class AccessLevel : ScriptableObject
{
    [SerializeField] List<AccessLevel> inheretedAccessLevels;

    [BoxGroup("Visuals")] public Color color = Color.white;
    [BoxGroup("Visuals")] public string displayName;

    [ShowInInspector] HashSet<AccessLevel> AllAccessLevels => GetAllAccessLevels(this);

    void OnValidate()
    {
        if (string.IsNullOrWhiteSpace(displayName)){
            displayName = name;
        }
    }

    public bool HasAccess(AccessLevel accessLevel)
    {
        return AllAccessLevels.Contains(accessLevel);
    }

    HashSet<AccessLevel> GetAllAccessLevels(AccessLevel mainAccessLevel)
    {
        HashSet<AccessLevel> allInheretedAccessLevels = new();
        allInheretedAccessLevels.Add(mainAccessLevel);
        foreach (var accessLevel in mainAccessLevel.GetInheretedAccessLevels()){
            if (accessLevel == null){
                Debug.LogWarning("Access Level is null", this);
                continue;
            }
            if (!allInheretedAccessLevels.Add(accessLevel)){
                continue;
            }
            allInheretedAccessLevels.AddRange(GetAllAccessLevels(accessLevel));
        }
        return allInheretedAccessLevels;
    }

    public List<AccessLevel> GetInheretedAccessLevels()
    {
        return inheretedAccessLevels;
    }
}