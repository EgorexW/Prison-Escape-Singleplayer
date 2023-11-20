using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Role : ScriptableObject
{
    [SerializeField] List<Role> allies = new();
    public RoleSpawner roleSpawner;

    public string GetName()
    {
        return name;
    }

    public bool IsAlly(Role role)
    {
        return allies.Contains(role);
    }
    void Reset(){
        allies.Add(this);
        roleSpawner = new();
    }
    void Awake(){
        roleSpawner = new();
    }
}

[Serializable]
public class RoleSpawner{
    public List<Transform> spawnPoints = new ();
    [HideInInspector] public int currentIndex;

    public Transform GetSpawnPoint(){
        currentIndex += 1;
        if (currentIndex > spawnPoints.Count){
            currentIndex = 1;
        }
        return spawnPoints[currentIndex - 1];
    }

    public void AddSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);
    }
    public void RemoveSpawnPoint(Transform transform)
    {
        spawnPoints.Remove(transform);
    }
}