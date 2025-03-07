using UnityEngine;

public class UIElement : MonoBehaviour
{
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
}