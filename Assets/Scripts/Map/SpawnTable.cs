using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
[InlineEditor]
public class SpawnTable : ScriptableObject, IGetGameObject
{
    [SerializeField] List<ObjectWithValue<Object>> gameObjects;

    public GameObject GetGameObject()
    {
        if (gameObjects.Count == 0){
            return null;
        }
        var rolledObj = WeightedChance.GetWeightedRoll(gameObjects);
        if (rolledObj.Object is GameObject){
            return rolledObj.Object as GameObject;
        }
        if (rolledObj.Object is IGetGameObject getGameObject){
            return getGameObject.GetGameObject();
        }
        Debug.LogError("Object is not GameObject or IGetGameObject is " + name, this);
        return null;
    }
}

public interface IGetGameObject
{
    public GameObject GetGameObject();
}