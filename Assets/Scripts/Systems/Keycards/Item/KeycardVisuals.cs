using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class KeycardVisuals : ItemVisuals
{
    [BoxGroup("References")] [Required] [SerializeField] Keycard keycard;

    [BoxGroup("References")] [SerializeField] List<Image> oneUseIcons;

    protected override void Apply()
    {
        if (keycard != null){
            displayName = keycard.accessLevel.displayName;
            if (keycard.oneUse){
                var pass = " Pass";
                displayName += pass;
            }
            color = keycard.accessLevel.color;
            foreach (var oneUseIcon in oneUseIcons) oneUseIcon.gameObject.SetActive(keycard.oneUse);
        }
        base.Apply();
    }
}