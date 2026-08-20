using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoisonSpreadBlocker : PoweredDevice{
    public static readonly HashSet<LevelNode> LevelNodesBlocked = new HashSet<LevelNode>();

    LevelNode node;
    
    void Awake(){
        node = LevelNodeExtension.GetNode(transform.position);
        LevelNodesBlocked.Add(node);
    }

    protected override void OnPowerChanged(){
        base.OnPowerChanged();
        if (IsPowered()){
            LevelNodesBlocked.Add(node);
        }
        else{
            LevelNodesBlocked.Remove(node);
        }
    }
}