using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoisonSpread : MonoBehaviour
{
    [BoxGroup("References")][Required][SerializeField] PoisonTrap thisPoisonTrap;
    [BoxGroup("References")][Required][SerializeField] SpawnTable poisonPrefab;


    [SerializeField] float rollInterval = 60f;
    [SerializeField] float spreadChance = 0.5f;
    
    static Dictionary<LevelNode, List<PoisonTrap>> poisonTrapsByNode = new();
    
    float nextRoll;
    public LevelNode Node => thisPoisonTrap.Node;

    void Start(){
        nextRoll = Time.time + rollInterval;
        poisonTrapsByNode[Node] = poisonTrapsByNode.GetValueOrDefault(Node, new List<PoisonTrap>());
        poisonTrapsByNode[Node].Add(thisPoisonTrap);
    }

    void Update(){
        if (Time.time > nextRoll){
            nextRoll = Time.time + rollInterval;
            Spread();
        }
    }

    void Spread(){
        var neighboringNodes = Node.GetNeighboringNodes();
        foreach (var neighboringNode in neighboringNodes){
            if (poisonTrapsByNode.ContainsKey(neighboringNode) && poisonTrapsByNode[neighboringNode].Count != 0){
                Debug.Log($"Not spreading to {neighboringNode.name} because it already has a poison trap");
                continue;
            }
            if (PoisonSpreadBlocker.LevelNodesBlocked.Contains(neighboringNode)){
                Debug.Log($"Not spreading to {neighboringNode.name} because it is blocked by a poison spread blocker");
                continue;
            }
            if (Random.value < spreadChance){
                Debug.Log($"Spreading poison trap to {neighboringNode.name}");
                var obj = Instantiate(poisonPrefab.GetGameObject(), neighboringNode.transform.position, Quaternion.identity);
                var trap =  obj.GetComponent<PoisonTrap>();
                trap.SetNode(neighboringNode);
                trap.Activate();
            }
        }
    }

    void OnDisable(){
        poisonTrapsByNode[Node].Remove(thisPoisonTrap);
    }
}
