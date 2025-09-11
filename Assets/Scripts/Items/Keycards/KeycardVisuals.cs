using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class KeycardVisuals : ItemVisuals
    {
        [BoxGroup("References")][Required][SerializeField] Keycard keycard;

        void Awake()
        {
            displayName = keycard.accessLevel.displayName;
            color = keycard.accessLevel.color;
        }
    }