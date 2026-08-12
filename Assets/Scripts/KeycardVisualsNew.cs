using Sirenix.OdinInspector;
using UnityEngine;

public class KeycardVisualsNew : MonoBehaviour
{
    [BoxGroup("References")] [SerializeField] Keycard keycard;
    
    [SerializeField] private Renderer stripeRenderer;
    [SerializeField] private GameObject oneUseIcon;
    [SerializeField] GameObject textParent;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");

    private MaterialPropertyBlock _propBlock;

    private void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        SetStripeColor(keycard.accessLevel.color);
        SetOneUse(keycard.oneUse, keycard.accessLevel.color);
        var displayName = keycard.accessLevel.displayName;
        if (keycard.oneUse){
            displayName += " Pass";
        }
        SetText(displayName);
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
            var iconRenderer = oneUseIcon.GetComponent<Renderer>();
            iconRenderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor(BaseColorID, contrastColor);
            iconRenderer.SetPropertyBlock(_propBlock);
        } 
        else 
        {
            oneUseIcon.SetActive(false);
        }
    }

    void SetText(string displayName){
        var textMesh = textParent.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (textMesh != null){
            textMesh.text = displayName;
        }
    }

    public void SetStripeColor(Color color)
    {
        stripeRenderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor(BaseColorID, color);
        stripeRenderer.SetPropertyBlock(_propBlock);
    }
}