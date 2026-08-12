using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEngine;

public class KeycardModelVisuals : MonoBehaviour
{
    [BoxGroup("References")] [SerializeField] Keycard keycard;
    
    [SerializeField] private Renderer stripeRenderer;
    [SerializeField] private GameObject oneUseIcon;
    [SerializeField] GameObject textParent;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");


    private MaterialPropertyBlock _propBlock;

    private void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        SetStripeColor(keycard.accessLevel.color);
        SetOneUse(keycard.oneUse, keycard.accessLevel.color);
        var displayName = ModifyName();
        SetText(displayName);
    }

    string ModifyName(){
        var displayName = keycard.accessLevel.displayName;
        if (keycard.oneUse){
            displayName += " Pass";
        }
        return displayName;
    }

    void SetOneUse(bool oneUse, Color stripeColor)
    {
        if (oneUse)
        {
            oneUseIcon.SetActive(true);

            // 1. Calculate luminance (perceived brightness) of the stripe color
            float luminance = (0.299f * stripeColor.r) + (0.587f * stripeColor.g) + (0.114f * stripeColor.b);

            // 2. Choose white for dark backgrounds, black for light backgrounds
            Color contrastColor = luminance < 0.5f ? Color.white : Color.black;

            // 3. Apply to property block
            foreach (var iconRenderer in oneUseIcon.GetComponentsInChildren<Renderer>()){
                iconRenderer.GetPropertyBlock(_propBlock);
                _propBlock.SetColor(BaseColorID, contrastColor);
                iconRenderer.SetPropertyBlock(_propBlock);
            }
        } 
        else 
        {
            oneUseIcon.SetActive(false);
        }
    }

    void SetText(string displayName){
        foreach (var textMesh in textParent.GetComponentsInChildren<TMPro.TextMeshPro>()){
            textMesh.text = displayName;
            
        }
        
    }

    public void SetStripeColor(Color color)
    {
        stripeRenderer.GetPropertyBlock(_propBlock);
    
        // 1. Set the base color
        _propBlock.SetColor(BaseColorID, color);
    
        // 2. Calculate -4 EV intensity (color * 2^-4)
        Color emissionColor = color * Mathf.Pow(2f, -4f);
    
        // 3. Set the emission color property
        _propBlock.SetColor(EmissionColorID, emissionColor);
    
        stripeRenderer.SetPropertyBlock(_propBlock);
    }
}