using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

class AIAggro : MonoBehaviour
{
    [Required][SerializeField] AIChaser aiChaser;

    public void Aggro(List<GameObject> targets)
    {
        aiChaser.Init(targets[0]);
    }
}