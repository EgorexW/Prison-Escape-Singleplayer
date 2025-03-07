using UnityEngine;

public class AINode : MonoBehaviour
{
    [SerializeField] AINodeType type;
    
    void Start()
    {
        General.GetObjectRoot(transform)?.GetComponent<AINodes>().AddNode(this);
    }
}

enum AINodeType
{
    Corridor
}