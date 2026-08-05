using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

public class KeycardVisualsNew : MonoBehaviour
{
    [BoxGroup("References")] [Required] public Keycard keycard;
    
    [Header("Components")]
    [SerializeField] private Renderer cardRenderer;
    [SerializeField] private TMP_Text titleText;

    // We cache property IDs for speed
    private static readonly int StripeColorID = Shader.PropertyToID("_Stripe_Color");
    // private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");

    private MaterialPropertyBlock _propBlock;

    private void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
    }

    void SetupAutomatic(){
        SetupCard(keycard.accessLevel.displayName, keycard.accessLevel.color);
    }
    
    private void Start()
    {
        SetupAutomatic();
    }


    /// <summary>
    /// Call this when generating a card at runtime!
    /// </summary>
    public void SetupCard(string cardName, Color stripeColor)
    {
        // 1. Update text via TextMeshPro 3D
        if (titleText != null)
        {
            titleText.text = cardName;
        }

        // 2. Fetch current property block from renderer
        cardRenderer.GetPropertyBlock(_propBlock);

        // 3. Inject custom colors into the block
        _propBlock.SetColor(StripeColorID, stripeColor);
        // _propBlock.SetColor(BaseColorID, cardBodyColor);

        // 4. Push the custom properties back to the card (Zero Material Creation!)
        cardRenderer.SetPropertyBlock(_propBlock);
    }
}