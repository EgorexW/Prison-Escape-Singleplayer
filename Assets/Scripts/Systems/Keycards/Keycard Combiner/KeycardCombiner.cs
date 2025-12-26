using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class KeycardCombiner : PoweredDevice
{
    [SerializeField] float losePowerChance = 0.5f;
    
    [BoxGroup("References")] [Required] [SerializeField] BoxCollider trigger;
    [BoxGroup("References")][Required][SerializeField] KeycardRecipes recipes;
    [BoxGroup("References")][Required][SerializeField] Transform spawnPoint;

    [FoldoutGroup("Events")]
    public UnityEvent onSuccesfullyCombined;

    [FoldoutGroup("Events")]
    public UnityEvent onFailedCombine;

    [Button]
    public void Combine()
    {
        if (!IsPowered()){
            return;
        }
        var colliders = General.OverlapBounds(trigger.bounds);
        var keycards = General.GetComponentsFromColliders<Keycard>(colliders);
        if (keycards.Count != 2){
            Debug.Log("Keycard count not supported: " + keycards.Count);
            onFailedCombine.Invoke();
            return;
        }
        if (keycards[0].accessLevel == keycards[1].accessLevel){
            Debug.Log("Keycards have the same access level: " + keycards[0].accessLevel);
            onFailedCombine.Invoke();
            return; 
        }
        var resultKeycard = recipes.CreateAndGetResult(keycards[0], keycards[1]);
        Destroy(keycards[0].gameObject);
        Destroy(keycards[1].gameObject);
        resultKeycard.gameObject.transform.parent = spawnPoint;
        resultKeycard.gameObject.transform.localPosition = Vector3.zero;
        resultKeycard.gameObject.transform.localRotation = Quaternion.identity;
        onSuccesfullyCombined.Invoke();
        if (Random.value < losePowerChance){
            MainPowerSystem.i.ChangePower(transform.position, PowerLevel.NoPower);
        }
    }
}
