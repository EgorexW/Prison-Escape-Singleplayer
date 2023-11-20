using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ServerGameObjectSpawner : MonoBehaviour
{
    [SerializeField] ScriptableObject getGameObject;
    [SerializeField][Range(0, 1)] float spawnChance = 1;
    [SerializeField] bool randomRotation = true;
    IGetGameObject GetGameObject {
        get {
            Debug.Assert(getGameObject is IGetGameObject, "getGameObject is not IGetGameObject", getGameObject);
            return (IGetGameObject) getGameObject;
        }
    }

    private void Awake()
    {
        SpawnGameObject();
    }

    private void SpawnGameObject()
    {
        if (Random.value > spawnChance){
            Destroy(gameObject); 
            return;
        }
        GameObject prefab = GetGameObject.GetGameObject();
        if (prefab == null){
            return;
        }
        Quaternion rotation = transform.rotation;
        if (randomRotation){
            rotation = Random.rotationUniform;
        }
        Instantiate(prefab, transform.position, rotation, transform.parent);
    }
    void Update(){
        Destroy(gameObject);
    }
}
