using System;
using UnityEngine;

public class Flashlight : Equipment
{
    [SerializeField] HeadlightSettings headlightSettings;

    protected override void Apply(Player player)
    {
        player.headlight.ApplySettings(headlightSettings);
    }
}