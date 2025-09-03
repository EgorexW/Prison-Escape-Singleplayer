using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class LootSpawnerObsolete : MonoBehaviour
{ 
    [InlineEditor][FormerlySerializedAs("lootTable")] [FormerlySerializedAs("gameObjectCollection")] [SerializeField] SpawnTable spawnTable;
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
        var gameObject = spawnTable.GetGameObject();
        if (gameObject == null){
            Debug.LogWarning("GameObject is null", this);
            return;
        }
        var rotation = transform.rotation;
        if (randomRotation){
            rotation = Random.rotationUniform;
        }
        Instantiate(gameObject, transform.position, rotation, transform.parent);
        Destroy(this.gameObject);
    }
}
