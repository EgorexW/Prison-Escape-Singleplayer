using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Poster : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Renderer rend;

    [SerializeField] float destroyChance = 0.5f;
    [SerializeField] Material[] materials;

    void Awake()
    {
        if (Random.value < destroyChance){
            Destroy(gameObject);
        }
        rend.material = materials[Random.Range(0, materials.Length)];
    }
}