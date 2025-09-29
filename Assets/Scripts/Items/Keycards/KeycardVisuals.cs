using Sirenix.OdinInspector;
using UnityEngine;

public class KeycardVisuals : ItemVisuals
{
    [BoxGroup("References")] [Required] [SerializeField] Keycard keycard;

    protected override void Apply()
    {
        if (keycard != null){
            displayName = keycard.accessLevel.displayName;
            if (keycard.oneUse){
                displayName += " Pass";
            }
            color = keycard.accessLevel.color;
        }
        base.Apply();
    }
}