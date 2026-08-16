using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class KeycardUIVisuals : ItemVisuals
{
    [BoxGroup("References")] [Required] public Keycard keycard;

    [BoxGroup("References")] [SerializeField] List<Image> oneUseIcons;

    public override void Apply()
    {
        if (keycard != null){
            displayName = keycard.accessLevel.displayName;
            if (keycard.OneUse){
                var pass = " Pass";
                displayName += pass;
            }
            color = keycard.accessLevel.color;
            foreach (var oneUseIcon in oneUseIcons) oneUseIcon.gameObject.SetActive(keycard.OneUse);
        }
        base.Apply();
    }
}