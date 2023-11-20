using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using UnityEngine.Serialization;

public class ServerGameObjectSpawner : NetworkBehaviour
{
    [FormerlySerializedAs("gameObjectCollection")][SerializeField] ScriptableObject getGameObject;
    [SerializeField][Range(0, 1)] float spawnChance = 1;
    [SerializeField] bool randomRotation = true;
    IGetGameObject GetGameObject {
        get {
            Debug.Assert(getGameObject is IGetGameObject, "getGameObject is not IGetGameObject", getGameObject);
            return (IGetGameObject) getGameObject;
        }
    }
    private GameObject createdGameObject;

    public override void OnStartServer()
    {
        base.OnStartServer();
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
        createdGameObject = Instantiate(prefab, transform.position, rotation);
    }
    void Update(){
        if (createdGameObject != null){
            Spawn(createdGameObject);
        }
        Destroy(gameObject);
    }
}
