using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnObjectItemEffect : UseableItem
{
    [BoxGroup("References")][Required][SerializeField] GameObject prefab;
    [BoxGroup("References")][Required][SerializeField] Transform spawnPoint;

    protected override void Apply()
    {
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        DestroyItem();
    }
}
