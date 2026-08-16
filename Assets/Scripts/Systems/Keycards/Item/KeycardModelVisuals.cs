using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class KeycardModelVisuals : MonoBehaviour
{
    [BoxGroup("References")] [SerializeField] Keycard keycard;
    
    [SerializeField][Required] private Renderer stripeRenderer;
    [SerializeField][Required] private GameObject oneUseIcon;
    [SerializeField][Required] GameObject textParent;
    [SerializeField][Required] GameObject useStatusParent;
    [SerializeField][Required] Renderer modelRenderer;
    
    [SerializeField][Required][BoxGroup("Model Materials")] Material cheapMaterial;
    [SerializeField][Required][BoxGroup("Model Materials")] Material expensiveMaterial;
    
    [SerializeField][Required][BoxGroup("Stripe Materials")] Material defaultStripeMaterial;
    [SerializeField][Required][BoxGroup("Stripe Materials")] Material weaponsStripeMaterial;
    [SerializeField][Required][BoxGroup("Stripe Materials")] Material leadershipStripeMaterial;
    

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");


    private MaterialPropertyBlock _propBlock;

    private void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        keycard.onChanged.AddListener(UpdateModel);
    }

    void UpdateModel(Keycard arg0){
        UpdateModel();
    }

    private void Start(){
        UpdateModel();
    }

    void UpdateModel(){
        SetStripeColor(keycard.AccessLevel.color);
        SetStatus(keycard.Status, keycard.AccessLevel.color);
        var displayName = ModifyName();
        SetText(displayName);
        SetModel();
    }

    void SetModel(){
        var material = cheapMaterial;
        if (keycard.AccessLevel.visualFlags.HasFlag(AccessLevelVisualFlags.Expensive)){
            material = expensiveMaterial;
        }
        modelRenderer.material = material;
    }

    string ModifyName(){
        var displayName = keycard.AccessLevel.displayName;
        if (keycard.OneUse){
            displayName += " Pass";
        }
        return displayName;
    }

    void SetStatus(KeycardStatus status, Color stripeColor)
    {
        if (status == KeycardStatus.UseActive || status == KeycardStatus.UseInactive)
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

            foreach (var textMesh in useStatusParent.GetComponentsInChildren<TextMeshPro>()){
                var statusText = "Active";
                var color = Color.green;
                if (status == KeycardStatus.UseInactive){
                    statusText = "Inactive";
                    color = Color.red;
                }
                textMesh.text = statusText;
                textMesh.color = color;
            }
        } 
        else 
        {
            oneUseIcon.SetActive(false);
            useStatusParent.SetActive(false);
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
        
        if (keycard.AccessLevel.visualFlags.HasFlag(AccessLevelVisualFlags.WeaponsAccess)){
            stripeRenderer.material = weaponsStripeMaterial;
        } else if (keycard.AccessLevel.visualFlags.HasFlag(AccessLevelVisualFlags.Leadership)){
            stripeRenderer.material = leadershipStripeMaterial;
        } else {
            stripeRenderer.material = defaultStripeMaterial;
        }
    }
}