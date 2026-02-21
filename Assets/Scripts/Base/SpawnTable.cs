using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu]
[InlineEditor]
public class SpawnTable : ScriptableObject
{
    [SerializeField] List<ObjectWithValue<Object>> gameObjects;
    
#if UNITY_EDITOR
    [ShowInInspector][ReadOnly] List<GameObject> possibleGameObjects = new();
    [ShowInInspector][ReadOnly] List<SpawnTable> referencedByTables = new();
#endif

    public GameObject GetGameObject()
    {
        if (gameObjects.Count == 0){
            return null;
        }
        var rolledObj = gameObjects.GetWeightedRoll();
        if (rolledObj.Object is GameObject){
            return rolledObj.Object as GameObject;
        }
        if (rolledObj.Object is SpawnTable getGameObject){
            return getGameObject.GetGameObject();
        }
        Debug.LogError("Object is not GameObject or another SpawnTable is " + name, this);
        return null;
    }
    
#if UNITY_EDITOR
    void OnValidate()
    {
        possibleGameObjects.Clear();
        Queue<ObjectWithValue<Object>> toProcess = new(gameObjects);
        while (toProcess.Count > 0){
            var item = toProcess.Dequeue();
            switch (item.Object){
                case SpawnTable getGameObject:
                    if (!getGameObject.referencedByTables.Contains(this)){
                        getGameObject.referencedByTables.Add(this);
                    }
                    foreach (var obj in getGameObject.gameObjects){
                        toProcess.Enqueue(obj);
                    }
                    break;
                case GameObject gameObj:
                    if (!possibleGameObjects.Contains(gameObj)){
                        possibleGameObjects.Add(gameObj);
                    }
                    break;
            }
        }
        
        foreach (var table in referencedByTables.Copy()){
            if (table.gameObjects.All(x => x.Object != this)){
                referencedByTables.Remove(table);
            }
        }
    }
#endif
}