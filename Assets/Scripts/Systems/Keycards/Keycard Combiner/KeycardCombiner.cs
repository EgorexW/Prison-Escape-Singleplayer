using Sirenix.OdinInspector;
using UnityEngine;

public class KeycardCombiner : PoweredDevice
{
    [SerializeField] float losePowerChance = 0.5f;
    
    [BoxGroup("References")] [Required] [SerializeField] BoxCollider trigger;
    [BoxGroup("References")][Required][SerializeField] KeycardRecipes recipes;
    [BoxGroup("References")][Required][SerializeField] Transform spawnPoint;

    [Button]
    public void Combine()
    {
        if (!IsPowered()){
            return;
        }
        var colliders = General.OverlapBounds(trigger.bounds);
        var keycards = General.GetComponentsFromColliders<Keycard>(colliders);
        if (keycards.Count != 2){
            Debug.LogWarning("Keycard count not supported: " + keycards.Count);
            return;
        }
        var resultKeycard = recipes.CreateAndGetResult(keycards[0], keycards[1]);
        Destroy(keycards[0].gameObject);
        Destroy(keycards[1].gameObject);
        resultKeycard.gameObject.transform.parent = spawnPoint;
        resultKeycard.gameObject.transform.localPosition = Vector3.zero;
        resultKeycard.gameObject.transform.localRotation = Quaternion.identity;
        if (Random.value < losePowerChance){
            MainPowerSystem.i.ChangePower(transform.position, PowerLevel.NoPower);
        }
    }
}
