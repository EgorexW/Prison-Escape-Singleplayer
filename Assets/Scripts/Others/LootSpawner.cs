using UnityEngine;
using UnityEngine.Serialization;

public class LootSpawner : MonoBehaviour
{ 
    [FormerlySerializedAs("lootTable")] [FormerlySerializedAs("gameObjectCollection")] [SerializeField] SpawnTable spawnTable;
    [SerializeField][Range(0, 1)] float spawnChance = 1;
    [SerializeField] bool randomRotation = true;

    void Awake(){
        SpawnGameObject();
    }

    protected void SpawnGameObject()
    {
        if (Random.value > spawnChance * spawnTable.spawnChance){
            return;
        }
        GameObject gameObject = spawnTable.GetGameObject();
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
