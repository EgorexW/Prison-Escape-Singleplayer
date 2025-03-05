using UnityEngine;
using UnityEngine.Serialization;

public class LootSpawner : MonoBehaviour
{ 
    [FormerlySerializedAs("gameObjectCollection")] [SerializeField] LootTable lootTable;
    [SerializeField][Range(0, 1)] float spawnChance = 1;
    [SerializeField] bool randomRotation = true;

    void Awake(){
        SpawnGameObject();
    }

    protected void SpawnGameObject()
    {
        if (Random.value > spawnChance * lootTable.spawnChance){
            return;
        }
        GameObject gameObject = lootTable.GetGameObject();
        if (gameObject == null){
            Debug.LogWarning("GameObject is null", this);
            return;
        }
        Quaternion rotation = transform.rotation;
        if (randomRotation){
            rotation = Random.rotationUniform;
        }
        Instantiate(gameObject, transform.position, rotation, transform.parent);
        Destroy(this.gameObject);
    }
}
