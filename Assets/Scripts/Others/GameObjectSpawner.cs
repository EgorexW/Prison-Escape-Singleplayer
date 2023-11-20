using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{
   [SerializeField] GameObjectCollection gameObjectCollection;
   [SerializeField][Range(0, 1)] float spawnChance = 1;
   [SerializeField] bool randomRotation = true;

    void Awake(){
        SpawnGameObject();
    }

    protected void SpawnGameObject()
    {
        if (Random.value > spawnChance){
            return;
        }
        GameObject gameObject = gameObjectCollection.GetGameObject();
        if (gameObject == null){
            Debug.LogWarning("GameObject is null", this);
            return;
        }
        Quaternion rotation = transform.rotation;
        if (randomRotation){
            rotation = Random.rotationUniform;
        }
        Instantiate(gameObject, transform.position, rotation, transform.parent);
        Destroy(gameObject);
    }
}
