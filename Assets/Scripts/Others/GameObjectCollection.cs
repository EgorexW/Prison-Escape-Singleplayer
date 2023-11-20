using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameObjectCollection : ScriptableObject, IGetGameObject
{
    [SerializeField] ObjectWithValue<Object>[] gameObjects;

    public GameObject GetGameObject(){
        if (gameObjects.Length == 0){
            return null;
        }
        ObjectWithValue<Object> rolledObj = WeightedChance.GetWeightedRoll<Object>(gameObjects);
        if (rolledObj.Object is GameObject){
            return rolledObj.Object as GameObject;
        }
        if (rolledObj.Object is IGetGameObject getGameObject)
        {
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