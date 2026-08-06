using Sirenix.OdinInspector;
using UnityEngine;

public class KeycardVisualsNew : MonoBehaviour
{
    [BoxGroup("References")] [Required] public Keycard keycard;
    [SerializeField] private Renderer stripeRenderer;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");

    private MaterialPropertyBlock _propBlock;

    private void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        SetStripeColor(keycard.accessLevel.color);
    }

    public void SetStripeColor(Color color)
    {
        stripeRenderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor(BaseColorID, color);
        stripeRenderer.SetPropertyBlock(_propBlock);
    }
}