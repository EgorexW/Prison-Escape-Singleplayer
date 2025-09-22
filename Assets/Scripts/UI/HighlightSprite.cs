using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HighlightSprite : MonoBehaviour
{
    [GetComponent] [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Color newColor;
    [SerializeField] Material newMaterial;

    Color originalColor;
    Material originalMaterial;

    void Awake()
    {
        originalColor = spriteRenderer.color;
        originalMaterial = spriteRenderer.material;
    }

    public void SetHighlight(bool highlight)
    {
        if (highlight){
            Highlight();
        }
        else{
            RemoveHighlight();
        }
    }

    void RemoveHighlight()
    {
        spriteRenderer.material = originalMaterial;
        spriteRenderer.color = originalColor;
    }

    public void Highlight()
    {
        spriteRenderer.material = newMaterial;
        spriteRenderer.color = newColor;
    }
}