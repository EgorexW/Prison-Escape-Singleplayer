using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CorridorNode : LevelNode
    {
        [HideInEditorMode]
        [ShowInInspector]
        public CorridorNodeType CorridorNodeType{
            get{
                return this.Connections().Count switch{
                    1 => CorridorNodeType.DeadEnd,
                    2 => Vector3.Dot(this.Connections()[0], this.Connections()[1]) < -0.5
                        ? CorridorNodeType.Straight
                        : CorridorNodeType.Turn,
                    3 => CorridorNodeType.ThreeWay,
                    4 => CorridorNodeType.FourWay,
                    _ => default
                };
            }
        }
        public override NodeType type => NodeType.Corridor;
    }
    
[Flags]
public enum CorridorNodeType
{
    Straight = 1 << 0,
    Turn = 1 << 1,
    ThreeWay = 1 << 2,
    FourWay = 1 << 3,
    DeadEnd = 1 << 4
}