using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Room : ScriptableObject
{
    public GameObject[] prefab = new GameObject[1];
    public RoomTrait[] traits;

    internal GameObject GetGameObject()
    {
        if (prefab[0] == null){
            Debug.LogWarning("prefab is not assigned", this);
        }
        return prefab[Random.Range(0, prefab.Length)];
    }
}