using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathfindingManager : MonoBehaviour
{
    void Awake(){
        General.CallAfterSeconds(() => GetComponent<AstarPath>().Scan(), 0.5f);
    }
}
