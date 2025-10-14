using Sirenix.OdinInspector;
using UnityEngine;

public class FirstGamePoster : MonoBehaviour
{
    [BoxGroup("References")] [Required] [SerializeField] Renderer rend;

    [BoxGroup("References")] [Required] [SerializeField] Material mat;

    void Start()
    {
        if (PlayerPrefs.GetInt("Games Started", 1) == 1){
            rend.material = mat;
        }
    }
}